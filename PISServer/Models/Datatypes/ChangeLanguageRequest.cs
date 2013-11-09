using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PISServer.Models.Datatypes
{
    public class ChangeLanguageRequest
    {
        public string Mail { get; set; }
        public string Language { get; set; }
    }
}