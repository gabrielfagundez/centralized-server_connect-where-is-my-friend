using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

// Import models
using PISServer.Models;

namespace PISServer.Controllers
{
    public class SignUpController : ApiController
    {
        //
        // GET api/signup
        //
        // Used when a client is trying to login to the System.
        public string GetSignUp([FromUri] string user, [FromUri] string password)
        {
            return "Usted ha ingresado los siguientes datos: user: " + user +
                ", password: " + password +
                ". Por favor, espere mientras desarrollamos completamente esta funcion";
        }
    }
}
