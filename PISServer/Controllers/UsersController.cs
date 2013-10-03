using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

// Import models
using PISServer.Models.Datatypes;

namespace PISServer.Controllers
{
    public class UsersController : ApiController
    {

        //
        // GET api/users/:id
        //
        // This method name also starts with "Get", but the method has a parameter named id. 
        // This parameter is mapped to the "id" segment of the URI path. 
        // The ASP.NET Web API framework automatically converts the ID to the 
        // correct data type (int) for the parameter.
        public User GetUser(int id)
        {
            using (var context = new DevelopmentPISEntities())
            {
                var user = context.Users.Find(id);
                if (user == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                return user;
            }
        }

        // 
        // GET api/users 
        //
        // The method name starts with "Get", so by convention it maps to GET requests. 
        // Also, because the method has no parameters, it maps to a URI that does 
        // not contain an "id" segment in the path.
        public List<User> GetAll([FromUri] string secret_token)
        {
            if (secret_token == "abcdefgh")
            {
                using (var context = new DevelopmentPISEntities())
                {
                    var users = context.Users.ToList();
                    return users;
                }
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

        }

        [HttpPost]
        public string LinkFacebookAccount()
        {
            using (var context = new DevelopmentPISEntities())
            {
                return "Method Not Implemented";
            }
        }

        [HttpPost]
        public string LinkLinkedInAccount()
        {
            using (var context = new DevelopmentPISEntities())
            {
                return "Method Not Implemented";
            }
        }

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
        [ActionName("Login")]
        public User PostLogin([FromBody] UserLoginRequest request)
        {
            if (request.Mail == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            using (var context = new DevelopmentPISEntities())
            {
                User user;

                // Find the user
                user = context.Users
                            .Where(u => u.Mail == request.Mail)
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

        //
        // POST api/signup
        //
        // Used when a client is trying to signup to the System.
        //
        // @param [String] mail
        // @param [String] name
        // @param [String] password
        //
        [ActionName("SignUp")]
        public User PostSignUp([FromBody] UserRequest request)
        {
            if (request.Mail == null || request.Password == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            using (var context = new DevelopmentPISEntities())
            {
                // Find if there is another user with the same mail (that must be unique)
                User user = context.Users
                            .Where(u => u.Mail == request.Mail)
                            .FirstOrDefault();
                if (user != null)
                {
                    throw new HttpResponseException(HttpStatusCode.Gone);
                }

                User newUser = new User
                {
                    Id = 2,
                    Name = request.Name,
                    Mail = request.Mail,
                    FacebookId = request.FacebookId,
                    LinkedInId = request.LinkedInId,
                    Password = request.Password
                };

                context.Users.Add(newUser);
                context.SaveChanges();

                // I have to get it again from the database in order to receive the id that was assigned
                User userToReturn = context.Users
                            .Where(u => u.Mail == request.Mail)
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
