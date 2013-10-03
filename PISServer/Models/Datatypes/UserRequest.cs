using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PISServer.Models.Datatypes
{
    public class UserRequest
    {
        public string Name { get; set; }
        public string Mail { get; set; }
        public string FacebookId { get; set; }
        public string LinkedInId { get; set; }
        public string Password { get; set; }
    }
}