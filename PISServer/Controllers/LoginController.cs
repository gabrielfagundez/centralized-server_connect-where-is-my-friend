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
    public class LoginController : ApiController
    {
        // This holds an IUsertRepository instance.
        // This repository holds a collection of Users to handle operations.
        // TODO: Check this http://www.asp.net/web-api/overview/extensibility/using-the-web-api-dependency-resolver
        static readonly IUserRepository repository = new UserRepository();

        //
        // POST api/login
        //
        // Used when a client is trying to login to the System.
        //
        // @param [String] mail
        // @param [String] password
        //
        // @return [User] the information of the user.
        //
        public User PostLogin([FromBody] UserLoginRequest request)
        {
            if (request.Email == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            // Find the user
            User user = repository.GetByEmail(request.Email);
            
            // Cannot find the user
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            // Incorrect Password
            if (user.Password != request.Password)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            // User found, return information
            return user;
        }
    }
}
