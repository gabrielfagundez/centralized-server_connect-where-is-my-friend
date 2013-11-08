using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Http;
using System.Threading;
using System.IO;

using PisDataAccess;
using PISServer.Models.Datatypes;
namespace PISServer.Controllers
{
    public class InternalController : ApiController
    {
        // 
        // Returns the last location of a user given its mail
        //
        [HttpPost]
        public String UploadPermission([FromBody] PermissionRequest request)
        {
            // If path is null return Not Found
            if (request.Path == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            // If Platform is null return Not Found
            if (request.Platform == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            FileStream fs = null;
            try
            {
                fs = File.OpenRead(request.Path);
                byte[] contenido = new byte[fs.Length];
                fs.Read(contenido, 0, Convert.ToInt32(fs.Length));
                using (var context = new DevelopmentPISEntities())
                {
                    context.PermissionSet.Add(new Permission()
                    {
                        Platform = request.Platform,
                        Content = contenido
                    });
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
            return "ok";
        }

        [HttpGet]
        public String GetLog()
        {
            String ret = "";
            using (var context = new DevelopmentPISEntities())
            {
                //DataRepository<MensajeLog> repo = new DataRepository<MensajeLog>(new DevelopmentPISEntities());
                 context.MensajeLogSetSet
                    .OrderByDescending(m => m.Id)
                    .Take(100)
                    .Select(m => m.Mensaje)
                    .ToList()
                    .ForEach(m=> ret += m + "**************************************************");
            }
            return ret;

        }

        [HttpGet]
        public String ResetLog()
        {
            using (var context = new DevelopmentPISEntities())
            {
                foreach (var msj in context.MensajeLogSetSet.ToList())
                {
                    context.MensajeLogSetSet.Remove(msj);
                }
                context.SaveChanges();
            }
            return "ok";

        }
    }
}