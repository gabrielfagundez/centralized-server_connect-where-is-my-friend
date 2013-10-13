using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PISServer.Models.Datatypes
{
    public class LocationAddRequest
    {
        public string Mail { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}