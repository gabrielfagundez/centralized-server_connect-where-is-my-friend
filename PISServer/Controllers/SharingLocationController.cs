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
    public class SharingLocationController : ApiController
    {
        [HttpPost]
        public string AddSharingRelationship([FromBody] FriendRequest request)
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
                userTo = context.Users
                            .Where(u => u.Mail == request.MailTo)
                            .FirstOrDefault();

                if (userTo == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                };

                SharingRelationship sharingTo = new SharingRelationship();
                sharingTo.SharingWith = userFrom.Id;
                sharingTo.StartTime = DateTime.Now;
                userTo.SharingRelationship.Add(sharingTo);
                context.SaveChanges();

                return "OK";
            }
        }

        [HttpGet]
        public List<SharingResponse> GetAllLocations(int id)
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
                var users = user.SharingRelationship.ToList();
                var ret = new List<SharingResponse>();

                for (int i = 0; i < users.Count; i++)
                {
                    var u = context.Users
                            .Where(u => u.Id == users[i].Id)
                            .FirstOrDefault();

                    SharingResponse sharingResponse = new SharingResponse();
                    sharingResponse.Id = users[i].Id;
                    sharingResponse.Name = u.Name;
                    sharingResponse.Mail = u.Mail;
                    sharingResponse.Latitude = u.UserPosition.Latitude;
                    sharingResponse.Longitude = u.UserPosition.Longitude;

                    ret.Add(sharingResponse);
                }

                return ret;
            }
        }



        // Metodos que usan id en lugar de mails
        [HttpPost]
        public string AddSharingRelationshipFromIds([FromBody] FriendRequestId request)
        {
            // Tiene que devolver la informacion del segundo usuario (no la pass!)
            // Error: si no existe un id 404
            // Error: si no existe un id 410 gone
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

                // Buscamos el amigo de quien solicita la amistad
                userFrom = context.Users
                            .Where(u => u.Id == request.IdFrom)
                            .FirstOrDefault();

                if (userFrom == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                };

                User userTo;

                // Buscamos el amigo al cual vamos a agregarle el amigo
                userTo = context.Users
                            .Where(u => u.Id == request.IdTo)
                            .FirstOrDefault();

                if (userTo == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                };

                SharingRelationship sharingTo = new SharingRelationship();
                sharingTo.SharingWith = userFrom.Id;
                sharingTo.StartTime = DateTime.Now;
                userTo.SharingRelationship.Add(sharingTo);
                context.SaveChanges();

                return "OK";
            }
        }
    }
}
