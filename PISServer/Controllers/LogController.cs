using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PisDataAccess;
using PISServer.Models;

namespace PISServer.Controllers
{
    public class LogController : Controller
    {
        //
        // GET: /Log/

        [HttpGet]
        public String GetLog()
        {
            String ret;
            using (var context = new DevelopmentPISEntities())
            {
                //DataRepository<MensajeLog> repo = new DataRepository<MensajeLog>(new DevelopmentPISEntities());
                ret = context.MensajeLogSetSet
                    .OrderByDescending(m => m.Id)
                    .Take(100).ToList().ToString();
            }
            return ret;

        }
    }
}
