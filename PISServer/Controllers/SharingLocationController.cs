using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading;
//SACAAAAR es para escribir en el debug
using System.Diagnostics;

using PisDataAccess;
using PISServer.Models.Datatypes;

namespace PISServer.Controllers
{
    public class SharingLocationController : ApiController
    {
        private volatile bool _stop;
        public int minutes = 10;

        // This method will be called when the thread is started. 
        public void Start()
        {
            using (var context = new DevelopmentPISEntities())
            {
                while (!_stop)
                {
                    Debug.WriteLine("worker thread: working... *****************************************");

                    DateTime minAgo = DateTime.Now - new TimeSpan(0, minutes, 0);
                    // Find shares that should end
                    var oldShares = context.ShareSet
                                .Where(u => u.Active == true)
                                .Where(u => DateTime.Compare(u.Date, minAgo) <= 0)
                                .ToList();
                    for (int i = 0; i < oldShares.Count; i++)
                    {
                        oldShares[i].Active = false;
                        Debug.WriteLine("worker thread: una sharing que cambio *****************************************");
                    }
                    context.SaveChanges();
                    Debug.WriteLine("worker thread: FINISH working... *****************************************");
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
            return this.minutes;
        }
    }
}
