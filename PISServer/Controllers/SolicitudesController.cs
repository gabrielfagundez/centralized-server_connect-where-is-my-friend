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

                // Create the solicitation
                WhereSolicitation sol = new WhereSolicitation();
                sol.Receiver = userTo.Mail;
                sol.Sender = userFrom.Mail;
                context.SaveChanges();

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
                var solicitudes = context.SolicitationSet
                            .Where(s => s.Receiver == user.Mail).ToList();

                var ret = new List<SolicitudesResponse>();

                // Create the response
                for (int i = 0; i < solicitudes.Count; i++)
                {
                    SolicitudesResponse solResponse = new SolicitudesResponse();
                    var user_sol = context.Users
                        .Where(u => u.Mail == solicitudes[i].Receiver)
                        .FirstOrDefault();

                    solResponse.SolicitudId = solicitudes[i].Id;
                    solResponse.SolicitudFromNombre = user_sol.Name;

                    ret.Add(solResponse);
                }

                return ret;
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

                // Find the request
                var solicitud = context.SolicitationSet
                    .Where(s => s.Id == request.IdSolicitud)
                    .FirstOrDefault();

                if (solicitud == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                };

                // FALTA CHEQUEAR POR USUARIO

                WhereAcceptationEvent wa = new WhereAcceptationEvent();
                //COMENTE LA LINEA DE ABAJO PORQUE ME DABA ERROR
                //wa.WhereSolicitation = solicitud;
                //HASTA ACA COMENTE PORQUE ME DABA ERROR
                context.SaveChanges();

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
