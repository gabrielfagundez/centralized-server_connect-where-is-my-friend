﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PISServer.Models
{
    public class UserLoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}