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

        //Sets a solicitation as sent
        public void SetSolicitationSent(WhereSolicitationEvent wse)
        {
            using (var context = new DevelopmentPISEntities())
            {
                wse.Sent = true;
                context.SaveChanges();
            }
        }

        //Sets a negation as sent
        public void SetNegationSent(WhereNegationEvent wne)
        {
            using (var context = new DevelopmentPISEntities())
            {
                wne.Sent = true;
                context.SaveChanges();
            }
        }

        //Sets an acceptation as sent
        public void SetAcceptationSent(WhereAcceptationEvent wae)
        {
            using (var context = new DevelopmentPISEntities())
            {
                wae.Sent = true;
                context.SaveChanges();
            }
        }
    }
}