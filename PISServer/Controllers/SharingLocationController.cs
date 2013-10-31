using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading;

using PisDataAccess;
using PISServer.Models.Datatypes;

namespace PISServer.Controllers
{
    public class SharingLocationController : ApiController
    {
        private volatile bool _stop;
        public static int minutes = 10;

        // This method will be called when the thread is started. 
        public void Start()
        {
            using (var context = new DevelopmentPISEntities())
            {
                while (!_stop)
                {
                    
                    DateTime minAgo = DateTime.Now - new TimeSpan(0, minutes, 0);
                    // Find shares that should end
                    var oldShares = context.ShareSet
                                .Where(u => u.Active == true)
                                .Where(u => DateTime.Compare(u.Date, minAgo) <= 0)
                                .ToList();
                    for (int i = 0; i < oldShares.Count; i++)
                    {
                        oldShares[i].Active = false;
                    }
                    context.SaveChanges();
                    //Time is in miliseconds, so it works every 10 seconds
                    Thread.Sleep(10000);
                }
            }
        }

        public void RequestStop()
        {
            _stop = true;
        }

        [HttpGet]
        public int GetMinutes()
        {
            return minutes;
        }

        [HttpGet]
        public int SetMinutes(int m)
        {
            minutes = m;
            return minutes;
        }
    }
}
