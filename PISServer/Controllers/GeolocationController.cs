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
        [HttpPost]
        public LocationAddRequest SetLocation([FromBody] LocationAddRequest request)
        {
            using (var context = new DevelopmentPISEntities())
            {
                if (request.Mail == null || request.Longitude == null || request.Latitude == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                // Find the user
                var user = context.Users
                            .Where(u => u.Mail == request.Mail)
                            .FirstOrDefault();

                if (user == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                UserPosition up = new UserPosition();
                up.Latitude = request.Latitude;
                up.Longitude = request.Longitude;

                user.UserPosition = up;
                context.SaveChanges();

                return request;
            }
        }

        [HttpPost]
        public Location GetFriendLocation([FromBody] UserEmailRequest request)
        {
            using (var context = new DevelopmentPISEntities())
            {
                if (request.Mail == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                // Find the user
                var user = context.Users
                            .Where(u => u.Mail == request.Mail)
                            .FirstOrDefault();

                if (user == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                Location l = new Location();

                l.Latitude = user.UserPosition.Latitude;
                l.Longitude = user.UserPosition.Longitude;

                return l;
            }
        }
    }
}
