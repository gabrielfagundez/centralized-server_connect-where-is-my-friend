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
        // This holds an IUsertRepository instance.
        // This repository holds a collection of Users to handle operations.
        // TODO: Check this http://www.asp.net/web-api/overview/extensibility/using-the-web-api-dependency-resolver
        static readonly IUserRepository repository = new UserRepository();

        //
        // POST api/signup
        //
        // Used when a client is trying to signup to the System.
        //
        // @param [String] mail
        // @param [String] name
        // @param [String] password
        public User PostSignUp([FromUri] string mail, [FromUri] string name, [FromUri] string password)
        {
            User user = repository.GetByEmail(mail);
            if (user != null)
            {
                throw new HttpResponseException(HttpStatusCode.Gone);
            }

            // If the information is correct
            user = repository.AddUserWithData(mail, name, password, null, null);
            return user;
        }
    }
}
