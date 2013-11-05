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

        public ActionResult Index()
        {
            DataRepository<MensajeLog> repo = new DataRepository<MensajeLog>(new DevelopmentPISEntities());           
            return View( repo.Query()
                .OrderByDescending(m => m.Id)
                .Take(100)
                .Select(m => m.Mensaje)
                .ToList());
        }

    }
}
