using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PISServer.Models.Datatypes
{
    public class SolicitudRequest
    {
        public int IdUser { get; set; }
        public int IdSolicitud { get; set; }
    }
}