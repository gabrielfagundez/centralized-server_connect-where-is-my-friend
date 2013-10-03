using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PISServer.Controllers
{
    public class GeolocationController : ApiController
    {
        [HttpPost]
        public string SendLocation()
        {
            using (var context = new DevelopmentPISEntities())
            {
                return "Method Not Implemented";
            }
        }

        [HttpPost]
        public string GetFriendLocation()
        {
            using (var context = new DevelopmentPISEntities())
            {
                return "Method Not Implemented";
            }
        }
    }
}
