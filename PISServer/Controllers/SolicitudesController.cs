﻿using System;
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

                User userFor;

                // Find the other friend
                userFor = context.Users
                            .Where(u => u.Id == request.IdTo)
                            .FirstOrDefault();

                if (userFor == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                };

                 // Verify if exists a pending solicitation for the user
                 WhereSolicitation exists = context.WhereSolicitationSet
                             .Where(s => s.From == userFrom.Id && s.For == userFor.Id && s.WhereSolicitationEvent != null)
                             .FirstOrDefault();
 
                 if (exists != null)
                 {
                     throw new HttpResponseException(HttpStatusCode.BadRequest);
                 };

                // Verify if are friends
                var friends = userFor.FriendsOf
                            .Where(u => u.Id == userFrom.Id)
                            .FirstOrDefault();
                if (friends == null)
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                }
                
                // Create the solicitation
                WhereSolicitation sol = new WhereSolicitation();
                sol.WhereAcceptationEvent = null;
                sol.WhereNegationEvent = null;
                sol.From = userFrom.Id;
                sol.For = userFor.Id;

                // Associate the user with the FOR (which will receive the push)
                WhereSolicitationEvent wse = new WhereSolicitationEvent();
                userFor.Event.Add(wse);
                wse.User = userFor;

                sol.WhereSolicitationEvent = wse;
                sol.WhereNegationEvent = null;
                sol.WhereAcceptationEvent = null;

                context.WhereSolicitationSet.Add(sol);
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
                var solicitudes = context.WhereSolicitationSet
                            .Where(s => s.For == user.Id).ToList();

                var ret = new List<SolicitudesResponse>();

                // Create the response
                for (int i = 0; i < solicitudes.Count; i++)
                {
                    SolicitudesResponse solResponse = new SolicitudesResponse();

                    // Find the user
                    int idFrom = solicitudes[i].From;
                    var user_sol = context.Users
                            .Where(u => u.Id == idFrom)
                            .FirstOrDefault();

                    solResponse.SolicitudId = solicitudes[i].Id;
                    solResponse.SolicitudFromNombre = user_sol.Name;

                    if(solicitudes[i].WhereAcceptationEvent == null && solicitudes[i].WhereNegationEvent == null)
                    {
                        // Agregamos la solución
                        ret.Add(solResponse);
                    }
                }

                return ret;
            }
        }

        //
        // Allows to get all accepted request for a friend
        //
        [HttpGet]
        public List<SolicitudesResponse> GetAllAccepted(int id)
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
                var solicitudes = context.WhereSolicitationSet
                            .Where(s => s.For == user.Id).ToList();

                var ret = new List<SolicitudesResponse>();

                // Create the response
                for (int i = 0; i < solicitudes.Count; i++)
                {
                    SolicitudesResponse solResponse = new SolicitudesResponse();

                    // Find the user
                    var user_sol = context.Users.Find(solicitudes[i].From);

                    solResponse.SolicitudId = solicitudes[i].Id;
                    solResponse.SolicitudFromNombre = user_sol.Name;

                    if (solicitudes[i].WhereAcceptationEvent != null && solicitudes[i].WhereNegationEvent == null)
                    {
                        // Agregamos la solución
                        ret.Add(solResponse);
                    }
                }

                return ret;
            }
        }

        //
        // Allows to get all rejected request for a friend
        //
        [HttpGet]
        public List<SolicitudesResponse> GetAllRejected(int id)
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
                var solicitudes = context.WhereSolicitationSet
                            .Where(s => s.For == user.Id).ToList();

                var ret = new List<SolicitudesResponse>();

                // Create the response
                for (int i = 0; i < solicitudes.Count; i++)
                {
                    SolicitudesResponse solResponse = new SolicitudesResponse();

                    // Find the user
                    var user_sol = context.Users.Find(solicitudes[i].From);

                    solResponse.SolicitudId = solicitudes[i].Id;
                    solResponse.SolicitudFromNombre = user_sol.Name;

                    if (solicitudes[i].WhereAcceptationEvent == null && solicitudes[i].WhereNegationEvent != null)
                    {
                        // Agregamos la solución
                        ret.Add(solResponse);
                    }
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
                var solicitud = context.WhereSolicitationSet
                    .Where(s => s.Id == request.IdSolicitud)
                    .FirstOrDefault();

                if (solicitud == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                };

                if (solicitud.WhereAcceptationEvent != null || solicitud.WhereNegationEvent != null)
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                };

                if (user.Id != solicitud.For)
                {
                    throw new HttpResponseException(HttpStatusCode.Unauthorized);
                };

                // Find the user that will be notified with a PUSH notification
                var userFrom = context.Users
                            .Where(u => u.Id == solicitud.From)
                            .FirstOrDefault();

                // Create the accept
                WhereAcceptationEvent wa = new WhereAcceptationEvent();
                userFrom.Event.Add(wa);
                wa.User = userFrom;

                context.EventSet.Remove(solicitud.WhereSolicitationEvent);

                solicitud.WhereSolicitationEvent = null;
                solicitud.WhereNegationEvent = null;
                solicitud.WhereAcceptationEvent = wa;
                context.EventSet.Add(wa);
                

                //Create the sharing relation
                Share sh = new Share()
                {
                    Date = DateTime.Now,
                    Active = true,
                    UserId = solicitud.For,
                    UserId1 = solicitud.From,
                };
                user.ShareFrom.Add(sh);
                userFrom.ShareWith.Add(sh);
                
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
                var solicitud = context.WhereSolicitationSet
                    .Where(s => s.Id == request.IdSolicitud)
                    .FirstOrDefault();

                if (solicitud == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                };

                if (solicitud.WhereAcceptationEvent != null || solicitud.WhereNegationEvent != null)
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                };

                if (user.Id != solicitud.For)
                {
                    throw new HttpResponseException(HttpStatusCode.Unauthorized);
                };

                // Find the user that will be notified with a PUSH notification
                var userFrom = context.Users
                            .Where(u => u.Id == solicitud.From)
                            .FirstOrDefault();

                // Create the negate
                WhereNegationEvent wn = new WhereNegationEvent();
                userFrom.Event.Add(wn);
                wn.User = userFrom;
                
                context.EventSet.Remove(solicitud.WhereSolicitationEvent);

                solicitud.WhereSolicitationEvent = null;
                solicitud.WhereAcceptationEvent = null;
                solicitud.WhereNegationEvent = wn;
                context.EventSet.Add(wn);
                context.SaveChanges();

                return "OK";

            }
        }
    }
}
