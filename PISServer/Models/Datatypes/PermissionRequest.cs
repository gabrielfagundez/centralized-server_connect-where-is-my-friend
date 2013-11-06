using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PISServer.Models.Datatypes
{
    public class PermissionRequest
    {
        public string Path { get; set; }
        public string Platform { get; set; }
    }
}