using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Threading;
using PISServer.Controllers;

namespace PISServer
{
    // Nota: para obtener instrucciones sobre cómo habilitar el modo clásico de IIS6 o IIS7, 
    // visite http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Remove XML formatter, we only want to allow JSON
            HttpConfiguration httpConfig = GlobalConfiguration.Configuration;
            httpConfig.Formatters.Remove(httpConfig.Formatters.XmlFormatter);

            SharingLocationController slc = new SharingLocationController();
            Thread workerThread = new Thread(slc.Start);

            // Start the worker thread.
            workerThread.Start();
            // Loop until worker thread activates.
            while (!workerThread.IsAlive) ;

        }
    }
}