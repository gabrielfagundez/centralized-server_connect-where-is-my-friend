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
        [HttpPost]
        public UserResponse AddFriend([FromBody] FriendRequest request)
        {
            // Tiene que devolver la informacion del segundo usuario (no la pass!)
            // Error: si no existe un id 404
            // Error: si no existe un id 410 gone
            using (var context = new DevelopmentPISEntities())
            {
                if (request.MailFrom == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                };

                if (request.MailTo == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                };

                User userFrom;

                // Buscamos el amigo de quien solicita la amistad
                userFrom = context.Users
                            .Where(u => u.Mail == request.MailFrom)
                            .FirstOrDefault();

                if (userFrom == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                };

                User userTo;

                // Buscamos el amigo al cual vamos a agregarle el amigo
                userTo = context.Users
                            .Where(u => u.Mail == request.MailTo)
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

        [HttpGet]
        public List<UserResponse> GetAllFriends(int id)
        {
            using (var context = new DevelopmentPISEntities())
            {
                // Buscamos el usuario
                // Find the user
                var user = context.Users
                            .Where(u => u.Id == id)
                            .FirstOrDefault();

                if (user == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                // Buscamos sus amigos
                var users = user.FriendsOf.ToList();
                var ret = new List<UserResponse>();

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
    }
}
