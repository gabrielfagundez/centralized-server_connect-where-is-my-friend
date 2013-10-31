using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using PisDataAccess;

namespace PISServer.Models
{
    public class PushMiddleware
    {
        // Returns all the events that should be sent to accept/negate
        public List<WhereSolicitationEvent> GetUnsentEvents()
        {
            using (var context = new DevelopmentPISEntities())
            {
                return context.EventSet.OfType<WhereSolicitationEvent>().Where(s => s.Sent == false).ToList();
            }
        }

        // Return all accepted requests
        public List<WhereAcceptationEvent> GetAcceptedEvents()
        {
            using (var context = new DevelopmentPISEntities())
            {
                return context.EventSet.OfType<WhereAcceptationEvent>().Where(s => s.Sent == false).ToList();
            }
        }

        // Return all rejected requests
        public List<WhereNegationEvent> GetRejectedEvents()
        {
            using (var context = new DevelopmentPISEntities())
            {
                return context.EventSet.OfType<WhereNegationEvent>().Where(s => s.Sent == false).ToList();
            }
        }
    }
}