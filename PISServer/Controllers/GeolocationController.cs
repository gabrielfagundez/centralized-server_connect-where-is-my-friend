using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using PisDataAccess;
using PISServer.Models.Datatypes;

namespace PISServer.Controllers
{
    public class GeolocationController : ApiController
    {

        // 
        // Returns the last location of a user given its mail
        //
        [HttpPost]
        public Location GetFriendLocation([FromBody] UserEmailRequest request)
        {
            using (var context = new DevelopmentPISEntities())
            {
                // If mail is null return Not Found
                if (request.Mail == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                // Find the user
                var user = context.Users
                            .Where(u => u.Mail == request.Mail)
                            .FirstOrDefault();

                // If user is null return Not Found
                if (user == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                // If user position is null return not found
                if (user.UserPosition == null)
                {
                    Location null_location = new Location();

                    null_location.Latitude = null;
                    null_location.Longitude = null;

                    return null_location;
                }

                // Create the response
                Location l = new Location();

                l.Latitude = user.UserPosition.Latitude;
                l.Longitude = user.UserPosition.Longitude;

                return l;
            }
        }

        //
        // Returns the last position of each friend
        //
        [HttpPost]
        public List<LocationAddRequest> GetLastFriendsLocation([FromBody] UserEmailRequest request)
        {
            using (var context = new DevelopmentPISEntities())
            {
                // If mail is null return null
                if (request.Mail == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                // Find the user
                var user = context.Users
                            .Where(u => u.Mail == request.Mail)
                            .FirstOrDefault();

                // If user is null return Not Found
                if (user == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                // Find its friends who are sharing position
                var shares = context.ShareSet
                            .Where(u => u.UserWith.Mail == request.Mail)
                            .Where(u => u.Active == true)
                            .ToList();

                var ret = new List<LocationAddRequest>();
                User userSharing;
                
                for (int i = 0; i < shares.Count; i++)
                {
                    //Find the user
                    string m = shares[i].UserFrom.Mail;
                    userSharing = context.Users
                            .Where(u => u.Mail == m)
                            .FirstOrDefault();
                    
                    // Create a response
                    LocationAddRequest userLocation = new LocationAddRequest();
                    userLocation.Mail = userSharing.Mail;
                    userLocation.Name = userSharing.Name;
                    
                    // If user dont have position, return nulls
                    if (userSharing.UserPosition == null)
                    {
                        userLocation.Latitude = null;
                        userLocation.Longitude = null;
                    }
                    else
                    {
                        userLocation.Latitude = userSharing.UserPosition.Latitude;
                        userLocation.Longitude = userSharing.UserPosition.Longitude;
                    }

                    ret.Add(userLocation);
                }

                return ret;
            }
        }

        // 
        // Allow to set the last position of a friend
        //
        [HttpPost]
        public LocationAddRequest SetLocation([FromBody] LocationAddRequest request)
        {
            using (var context = new DevelopmentPISEntities())
            {
                // If the one of the fields is null return Not Found
                if (request.Mail == null || request.Longitude == null || request.Latitude == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                // Find the user
                var user = context.Users
                            .Where(u => u.Mail == request.Mail)
                            .FirstOrDefault();

                // If the user is null return Not Found
                if (user == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                if (user.UserPosition == null) {
                    UserPosition up = new UserPosition();
                    up.Latitude = request.Latitude;
                    up.Longitude = request.Longitude;
                    user.UserPosition = up;
                }
                else {
                    user.UserPosition.Latitude = request.Latitude;
                    user.UserPosition.Longitude = request.Longitude;
                }
                context.SaveChanges();

                return request;
            }
        }

        //
        // Get last friends location based on an id
        //
        [HttpGet]
        public List<LocationAddRequest> GetLastFriendsLocationsById(int id)
        {
            using (var context = new DevelopmentPISEntities())
            {
                // Find the user
                var user = context.Users
                            .Where(u => u.Id == id)
                            .FirstOrDefault();

                // If user is null return Not Found
                if (user == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                // Find its friends who are sharing position
                var shares = context.ShareSet
                            .Where(u => u.UserWith.Id == id)
                            .Where(u => u.Active == true)
                            .ToList();

                var ret = new List<LocationAddRequest>();
                User userSharing;

                for (int i = 0; i < shares.Count; i++)
                {
                    //Find the user
                    string m = shares[i].UserFrom.Mail;
                    userSharing = context.Users
                            .Where(u => u.Mail == m)
                            .FirstOrDefault();

                    // Create the response
                    LocationAddRequest userLocation = new LocationAddRequest();
                    userLocation.Mail = userSharing.Mail;
                    userLocation.Name = userSharing.Name;

                    // If user dont have position, return nulls
                    if (userSharing.UserPosition == null)
                    {
                        userLocation.Latitude = null;
                        userLocation.Longitude = null;
                    }
                    else
                    {
                        userLocation.Latitude = userSharing.UserPosition.Latitude;
                        userLocation.Longitude = userSharing.UserPosition.Longitude;
                    }
                    ret.Add(userLocation);
                }

                return ret;
            }
        }
    }
}
