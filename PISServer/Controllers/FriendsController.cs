using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using PISServer.Models.Datatypes;
using PisDataAccess;


namespace PISServer.Controllers
{
    public class FriendsController : ApiController
    {
        // 
        // Allows adding a friend.
        //
        [HttpPost]
        public UserResponse AddFriend([FromBody] FriendRequest request)
        {
            using (var context = new DevelopmentPISEntities())
            {
                // If one of the mails is null return Not Found
                if (request.MailFrom == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                };

                if (request.MailTo == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                };

                User userFrom;

                // Find the user that request the friendship
                userFrom = context.Users
                            .Where(u => u.Mail == request.MailFrom)
                            .FirstOrDefault();

                if (userFrom == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                };

                User userTo;

                // Find the user to add as friend of the first one
                userTo = context.Users
                            .Where(u => u.Mail == request.MailTo)
                            .FirstOrDefault();

                if (userTo == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                };

                // Add friends and save
                userFrom.FriendsOf.Add(userTo);
                userTo.FriendsOf.Add(userFrom);
                context.SaveChanges();

                // Create the response
                UserResponse userResponse = new UserResponse();
                userResponse.Id = userTo.Id;
                userResponse.Name = userTo.Name;
                userResponse.Mail = userTo.Mail;
                userResponse.FacebookId = userTo.FacebookId;
                userResponse.LinkedInId = userTo.LinkedInId;

                return userResponse;
            }
        }

        //
        // Allows to get all friends
        //
        [HttpGet]
        public List<UserResponse> GetAllFriends(int id)
        {
            using (var context = new DevelopmentPISEntities())
            {
                // Find the user
                var user = context.Users
                            .Where(u => u.Id == id)
                            .FirstOrDefault();

                if (user == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                // Find its friends
                var users = user.FriendsOf.ToList();
                var ret = new List<UserResponse>();

                // Create the response
                for (int i = 0; i < users.Count; i++)
                {
                    UserResponse userResponse = new UserResponse();
                    userResponse.Id = users[i].Id;
                    userResponse.Name = users[i].Name;
                    userResponse.Mail = users[i].Mail;
                    userResponse.FacebookId = users[i].FacebookId;
                    userResponse.LinkedInId = users[i].LinkedInId;

                    ret.Add(userResponse);
                }

                return ret;
            }
        }

        //
        // Allow to add a friend based on ids
        //
        [HttpPost]
        public UserResponse AddFriendFromIds([FromBody] FriendRequestId request)
        {
            using (var context = new DevelopmentPISEntities())
            {
                if (request.IdFrom == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                };

                if (request.IdTo == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                };

                User userFrom;

                // Find the requesting friend
                userFrom = context.Users
                            .Where(u => u.Id == request.IdFrom)
                            .FirstOrDefault();

                if (userFrom == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                };

                User userTo;

                // Find the other friend
                userTo = context.Users
                            .Where(u => u.Id == request.IdTo)
                            .FirstOrDefault();

                if (userTo == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                };

                userFrom.FriendsOf.Add(userTo);
                userTo.FriendsOf.Add(userFrom);
                context.SaveChanges();

                UserResponse userResponse = new UserResponse();
                userResponse.Id = userTo.Id;
                userResponse.Name = userTo.Name;
                userResponse.Mail = userTo.Mail;
                userResponse.FacebookId = userTo.FacebookId;
                userResponse.LinkedInId = userTo.LinkedInId;

                return userResponse;
            }
        }
    }
}
