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
    public class SolicitudesController : ApiController
    {
        //
        // Allow to add a request from Where Is My Friend?
        //
        [HttpPost]
        public FriendRequestId Send([FromBody] FriendRequestId request)
        {
            using (var context = new DevelopmentPISEntities())
            {
                // If bad parameters
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

                // Add the request

               



                return request;
            }
        }

        //
        // Allows to get all request for a friend
        //
        [HttpGet]
        public List<SolicitudesResponse> GetAll(int id)
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

                // Find its requests










                return null;
            }
        }

        //
        // Allows to get accept a request
        //
        [HttpPost]
        public string Accept([FromBody] SolicitudRequest request)
        {
            using (var context = new DevelopmentPISEntities())
            {
                // If bad parameters
                if (request.IdUser == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                };

                if (request.IdSolicitud == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                };

                User user;

                // Find the requesting friend
                user = context.Users
                            .Where(u => u.Id == request.IdUser)
                            .FirstOrDefault();

                if (user == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                };

                return "OK";

            }
        }

        //
        // Allows to get reject a request
        //
        [HttpPost]
        public string Reject([FromBody] SolicitudRequest request)
        {
            using (var context = new DevelopmentPISEntities())
            {
                // If bad parameters
                if (request.IdUser == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                };

                if (request.IdSolicitud == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                };

                User user;

                // Find the requesting friend
                user = context.Users
                            .Where(u => u.Id == request.IdUser)
                            .FirstOrDefault();

                if (user == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                };

                return "OK";

            }
        }
    }
}
