﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PISServer.Models.Datatypes
{
    public class UserLoginRequest
    {
        public string Mail { get; set; }
        public string Password { get; set; }
        public string DeviceId { get; set; }
        public string Platform { get; set; }
    }
}