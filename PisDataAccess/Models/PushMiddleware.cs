using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using PisDataAccess;

namespace PISServer.Models
{
    public class PushMiddleware
    {
        private DevelopmentPISEntities context;

        public PushMiddleware()
        {
            context = new DevelopmentPISEntities();
        }

        // Returns all the events that should be sent to accept/negate
        public List<WhereSolicitationEvent> GetUnsentEvents()
        {
            return context.EventSet.OfType<WhereSolicitationEvent>().Where(s => s.Sent == false).ToList();
        }

        // Return all accepted requests
        public List<WhereAcceptationEvent> GetAcceptedEvents()
        {
            return context.EventSet.OfType<WhereAcceptationEvent>().Where(s => s.Sent == false).ToList();
        }

        // Return all rejected requests
        public List<WhereNegationEvent> GetRejectedEvents()
        {
            return context.EventSet.OfType<WhereNegationEvent>().Where(s => s.Sent == false).ToList();
        }

        public String GetUserFromSolicitation(WhereSolicitation solicitation)
        {
            return context.Users
                        .Where(u => u.Id == solicitation.From)
                        .Select(u => u.Name)
                        .FirstOrDefault();
        }

        public String GetUserForSolicitation(WhereSolicitation solicitation)
        {
            return context.Users
                        .Where(u => u.Id == solicitation.For)
                        .Select(u => u.Name)
                        .FirstOrDefault();
        }


        public byte[] GetPermission(String platform)
        {
            return context.PermissionSet
                    .Where(p => p.Platform == platform)
                    .Select(p => p.Content)
                    .FirstOrDefault();
        }

        public void Log(String str)
        {
            context.MensajeLogSetSet.Add(new MensajeLogSet() { Mensaje = str });
        }

        public void Save()
        {
            context.SaveChanges();
        }


    }
}