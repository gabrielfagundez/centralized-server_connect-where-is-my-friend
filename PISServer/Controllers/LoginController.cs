using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

// Import models
using PISServer.Models;
using PISServer.Models.Datatypes;

namespace PISServer.Controllers
{
    public class LoginController : ApiController
    {
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
        public Users PostLogin([FromBody] UserLoginRequest request)
        {
            if (request.Email == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            using (var context = new DatabasePisEntities())
            {
                Users user;
                
                // Find the user
                user = context.Users
                            .Where(u => u.Mail == request.Email)
                            .FirstOrDefault();

                if (user == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                else
                {
                    // Incorrect Password
                    if (user.Password != request.Password)
                    {
                        throw new HttpResponseException(HttpStatusCode.Unauthorized);
                    }
                }
                
                return user;
            }
        }
    }
}
