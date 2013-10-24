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
        // Allow to add a friend based on ids
        //
        [HttpPost]
        public FriendRequestId Send([FromBody] FriendRequestId request)
        {
            using (var context = new DevelopmentPISEntities())
            {
                // Si no existe alguno de los parametros necesarions
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

                // Agregamos la solicitud

               



                return request;
            }
        }
    }
}
