﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

// Import models
using PISServer.Models;

namespace PISServer.Controllers
{
    public class LoginController : ApiController
    {
        // This holds an IUsertRepository instance.
        // This repository holds a collection of Users to handle operations.
        // TODO: Check this http://www.asp.net/web-api/overview/extensibility/using-the-web-api-dependency-resolver
        static readonly IUserRepository repository = new UserRepository();

        //
        // GET api/login
        //
        // Used when a client is trying to login to the System.
        public string GetLogin([FromUri] string user, [FromUri] string password)
        {
            return "Usted ha ingresado los siguientes datos: user: " + user + 
                ", password: " + password + 
                ". Por favor, espere mientras desarrollamos completamente esta funcion";
        }
    }
}
