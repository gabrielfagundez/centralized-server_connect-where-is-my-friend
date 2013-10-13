using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

// Import models
using PISServer.Models.Datatypes;
using PisDataAccess;

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
        public UserResponse GetUser(int id)
        {
            using (var context = new DevelopmentPISEntities())
            {
                // Find the user
                var user = context.Users
                            .Where(u => u.Id == id)
                            .FirstOrDefault();

                if (user == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                UserResponse userResponse = new UserResponse();
                userResponse.Id = user.Id;
                userResponse.Name = user.Name;
                userResponse.Mail = user.Mail;
                userResponse.FacebookId = user.FacebookId;
                userResponse.LinkedInId = user.LinkedInId;

                return userResponse;
            }
        }

        //
        // GET api/users/GetUserByMail
        //
        // This method name also starts with "Get", but the method has a parameter named id. 
        // This parameter is mapped to the "id" segment of the URI path. 
        // The ASP.NET Web API framework automatically converts the ID to the 
        // correct data type (int) for the parameter.
        public UserResponse GetUserByMail([FromBody] UserEmailRequest request)
        {
            using (var context = new DevelopmentPISEntities())
            {
                if (request.Mail == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                // Find the user
                var user = context.Users
                            .Where(u => u.Mail == request.Mail)
                            .FirstOrDefault();

                if (user == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                UserResponse userResponse = new UserResponse();
                userResponse.Id = user.Id;
                userResponse.Name = user.Name;
                userResponse.Mail = user.Mail;
                userResponse.FacebookId = user.FacebookId;
                userResponse.LinkedInId = user.LinkedInId;

                return userResponse;
            }
        }


        // 
        // GET api/users 
        //
        // The method name starts with "Get", so by convention it maps to GET requests. 
        // Also, because the method has no parameters, it maps to a URI that does 
        // not contain an "id" segment in the path.
        public List<UserResponse> GetAll([FromUri] string secret_token)
        {
            if (secret_token == "abcdefgh")
            {
                using (var context = new DevelopmentPISEntities())
                {
                    var users = context.Users.ToList();
                    var ret = new List<UserResponse>();

                    for (int i = 0; i < users.Count; i++)
                    {
                        UserResponse userResponse = new UserResponse();
                        userResponse.Id = users[i].Id;
                        userResponse.Name = users[i].Name;
                        userResponse.Mail = users[i].Mail;
                        userResponse.FacebookId = users[i].FacebookId;
                        userResponse.LinkedInId = users[i].LinkedInId;

                        ret.Add(userResponse);
                    }

                    return ret;
                    
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
        public UserResponse PostLogin([FromBody] UserLoginRequest request)
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

                UserResponse userResponse = new UserResponse();
                userResponse.Id = user.Id;
                userResponse.Name = user.Name;
                userResponse.Mail = user.Mail;
                userResponse.FacebookId = user.FacebookId;
                userResponse.LinkedInId = user.LinkedInId;

                return userResponse;
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
        public UserResponse PostSignUp([FromBody] UserRequest request) 
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
                    Name = request.Name,
                    Mail = request.Mail,
                    FacebookId = request.FacebookId,
                    LinkedInId = request.LinkedInId,
                    Password = request.Password,
                    Session = null,
                    UserPosition = null
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


                UserResponse userResponse = new UserResponse();
                userResponse.Id = userToReturn.Id;
                userResponse.Name = userToReturn.Name;
                userResponse.Mail = userToReturn.Mail;
                userResponse.FacebookId = userToReturn.FacebookId;
                userResponse.LinkedInId = userToReturn.LinkedInId;

                return userResponse;

            }
        }
    }
}
