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
        public Users PostSignUp([FromBody] UserRequest request)
        {
            if (request.Email == null || request.Password == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            System.Diagnostics.Debug.WriteLine("hola");
            using (var context = new DatabasePisEntities())
            {
                // Find if there is another user with the same mail (that must be unique)
                Users user = context.Users
                            .Where(u => u.Mail == request.Email)
                            .FirstOrDefault();
                if (user != null)
                {
                    throw new HttpResponseException(HttpStatusCode.Gone);
                }
                //Users newUser = new Users { Mail = "aaaaaaaaaa.fa07@gmail.com", FacebookId = "568349440", Password = "pass" };
                Users newUser = new Users { Name = request.Name, Mail = request.Email, FacebookId = request.FacebookId,
                                            LinkedInId = request.LinkedInId, Password = request.Password };
                
                context.Users.Add(newUser);
                context.SaveChanges();

                //I have to get it again from the database in order to receive the id that was asaigned
                Users userToReturn = context.Users
                            .Where(u => u.Mail == request.Email)
                            .FirstOrDefault();
                
                //This should NEVER happen
                if (userToReturn == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }


                return userToReturn;

            }
        }
    }
}
