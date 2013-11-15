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
        // Return a user identified by its id
        //
        public UserResponse GetUser(int id)
        {
            using (var context = new DevelopmentPISEntities())
            {
                // Find the user
                var user = context.Users
                            .Where(u => u.Id == id)
                            .FirstOrDefault();

                // If the user is null return Not Found
                if (user == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                // Create a response
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
        // 
        // Return a user identified by its email.
        //
        [HttpPost]
        public UserResponse GetUserByMail([FromBody] UserEmailRequest request)
        {
            using (var context = new DevelopmentPISEntities())
            {
                // If empty mail is received return Not Found
                if (request.Mail == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                // Find the user
                var user = context.Users
                            .Where(u => u.Mail == request.Mail)
                            .FirstOrDefault();

                // If the user is null return Not Found
                if (user == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                // Create the response
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
        //
        // Return the list of users
        //
        public List<UserResponse> GetAll([FromUri] string secret_token)
        {
            if (secret_token == "abcdefgh")
            {
                using (var context = new DevelopmentPISEntities())
                {
                    // Create a temporal list of users
                    var users = context.Users.ToList();
                    var ret = new List<UserResponse>();

                    // Navigate into the users
                    for (int i = 0; i < users.Count; i++)
                    {
                        UserResponse userResponse = new UserResponse();
                        userResponse.Id = users[i].Id;
                        userResponse.Name = users[i].Name;
                        userResponse.Mail = users[i].Mail;
                        userResponse.FacebookId = users[i].FacebookId;
                        userResponse.LinkedInId = users[i].LinkedInId;

                        // Add the current user
                        ret.Add(userResponse);
                    }

                    return ret;

                }
            }
            else
            {
                // If token is erroneous, return Unauthorized
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
        // Used when a client of Connect! is trying to login to the System.
        //
        [ActionName("Login")]
        public UserResponse PostLogin([FromBody] UserLoginRequest request)
        {
            // If mail is empty return Not Found
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

                // Return not found if user is null
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

                // Create a response
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
        // POST api/loginWhere
        //
        // Used when a client of Where? is trying to login to the System.
        //
        [ActionName("LoginWhere")]
        public UserResponse PostLoginWhere([FromBody] UserLoginWhereRequest request)
        {
            // If mail is empty return Not Found
            if (request.Mail == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if (request.DeviceId == null || request.DeviceId == "")
            {
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);
            }

            if (request.Platform == null)
            {
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);
            }

            if ((!request.Platform.Equals("ios", StringComparison.OrdinalIgnoreCase)) &&
                (!request.Platform.Equals("wp", StringComparison.OrdinalIgnoreCase)) &&
                (!request.Platform.Equals("android", StringComparison.OrdinalIgnoreCase)))
            {
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);
            }

            string lang;
            if (request.Language == null)
            {
                lang = "eng";
            }
            else
            {
                lang = request.Language;
            }

            using (var context = new DevelopmentPISEntities())
            {
                User user;

                // Find the user
                user = context.Users
                            .Where(u => u.Mail == request.Mail)
                            .FirstOrDefault();

                // Return not found if user is null
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

                // Create the session in the database
                Session ses = new Session();
                ses.DeviceId = request.DeviceId;
                ses.Platform = request.Platform;
                ses.UserId = user.Id;
                ses.User = user;
                ses.Active = true;
                ses.Date = DateTime.Now;
                ses.Language = lang;
                ses.Badge = 0;
                ses.BadgeAccept = 0;
                ses.BadgeSolicitation = 0;

                user.Session.Add(ses);
                context.SaveChanges();

                // Create a response
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
        [ActionName("SignUp")]
        public UserResponse PostSignUp([FromBody] UserRequest request)
        {
            // Return Not Found if mail or password is null
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

                // Create the user
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

                // Save the user
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

                // Create the response
                UserResponse userResponse = new UserResponse();
                userResponse.Id = userToReturn.Id;
                userResponse.Name = userToReturn.Name;
                userResponse.Mail = userToReturn.Mail;
                userResponse.FacebookId = userToReturn.FacebookId;
                userResponse.LinkedInId = userToReturn.LinkedInId;

                return userResponse;

            }
        }

        //
        // POST api/logoutWhere
        //
        // Used when a client of Where? is trying to login to the System.
        //
        [ActionName("LogoutWhere")]
        public string LogoutWhere([FromBody] UserEmailRequest request)
        {
            // If mail is empty return Not Found
            if (request.Mail == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            using (var context = new DevelopmentPISEntities())
            {
                // Find the user
                User user = context.Users
                            .Where(u => u.Mail == request.Mail)
                            .FirstOrDefault();

                // Return not found if user is null
                if (user == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                else
                {
                    List<Session> sesList;
                    sesList = context.SessionSet
                                .Where(s => s.Active == true)
                                .Where(s => s.UserId == user.Id)
                                .ToList();

                    //The user is not logued in
                    if (sesList.Count == 0)
                    {
                        throw new HttpResponseException(HttpStatusCode.Gone);
                    }


                    foreach (Session ses in sesList)
                    {
                        ses.Active = false;
                    }
                    context.SaveChanges();
                }

                return "ok";
            }
        }

        //
        // POST api/changeDeviceId
        //
        // Used when a client of Where? is trying to login to the System.
        //
        [ActionName("ChangeDeviceId")]
        public string PostChangeDeviceId([FromBody] ChangeDeviceIdRequest request)
        {
            // If mail is empty return Not Found
            if (request.Mail == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if (request.DeviceId == null || request.DeviceId == "")
            {
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);
            }

            if (request.Platform == null)
            {
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);
            }

            if ((!request.Platform.Equals("ios", StringComparison.OrdinalIgnoreCase)) &&
                (!request.Platform.Equals("wp", StringComparison.OrdinalIgnoreCase)) &&
                (!request.Platform.Equals("android", StringComparison.OrdinalIgnoreCase)))
            {
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);
            }

            using (var context = new DevelopmentPISEntities())
            {
                User user;

                // Find the user
                user = context.Users
                            .Where(u => u.Mail == request.Mail)
                            .FirstOrDefault();

                // Return not found if user is null
                if (user == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                else
                {
                    List<Session> sesList;
                    sesList = context.SessionSet
                                .Where(s => s.Active == true)
                                .Where(s => s.UserId == user.Id)
                                .ToList();

                    //The user is not logued in
                    if (sesList.Count == 0)
                    {
                        throw new HttpResponseException(HttpStatusCode.NotFound);
                    }


                    foreach (Session ses in sesList)
                    {
                        ses.DeviceId = request.DeviceId;
                        ses.Platform = request.Platform;
                    }
                    context.SaveChanges();
                }

                return "ok";
            }
        }

        //
        // POST api/changeLanguage
        //
        // Used when a client of Where? is trying to login to the System.
        //
        [ActionName("ChangeLanguage")]
        public string PostLogin([FromBody] ChangeLanguageRequest request)
        {
            // If mail is empty return Not Found
            if (request.Mail == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if (request.Language == null)
            {
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);
            }

            using (var context = new DevelopmentPISEntities())
            {
                User user;

                // Find the user
                user = context.Users
                            .Where(u => u.Mail == request.Mail)
                            .FirstOrDefault();

                // Return not found if user is null
                if (user == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                else
                {
                    List<Session> sesList;
                    sesList = context.SessionSet
                                .Where(s => s.Active == true)
                                .Where(s => s.UserId == user.Id)
                                .ToList();

                    //The user is not logued in
                    if (sesList.Count == 0)
                    {
                        throw new HttpResponseException(HttpStatusCode.NotFound);
                    }

                    foreach (Session ses in sesList)
                    {
                        ses.Language = request.Language;
                    }
                    context.SaveChanges();
                }

                return "ok";
            }
        }

        //
        // POST api/resetBadge
        //
        // Used when a client of Where? is trying to login to the System.
        //
        [ActionName("ResetBadge")]
        public string PostResetBadge([FromBody] UserEmailRequest request)
        {
            // If mail is empty return Not Found
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

                // Return not found if user is null
                if (user == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                else
                {
                    List<Session> sesList;
                    sesList = context.SessionSet
                                .Where(s => s.Active == true)
                                .Where(s => s.UserId == user.Id)
                                .ToList();

                    //The user is not logued in
                    if (sesList.Count == 0)
                    {
                        throw new HttpResponseException(HttpStatusCode.NotFound);
                    }

                    foreach (Session ses in sesList)
                    {
                        ses.Badge = 0;
                    }
                    context.SaveChanges();
                }

                return "ok";
            }
        }

        //
        // POST api/resetSolicitationBadge
        //
        // Used when a client of Where? is trying to login to the System.
        //
        [ActionName("ResetSolicitationBadge")]
        public string PostResetSolicitationBadge([FromBody] UserEmailRequest request)
        {
            // If mail is empty return Not Found
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

                // Return not found if user is null
                if (user == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                else
                {
                    List<Session> sesList;
                    sesList = context.SessionSet
                                .Where(s => s.Active == true)
                                .Where(s => s.UserId == user.Id)
                                .ToList();

                    //The user is not logued in
                    if (sesList.Count == 0)
                    {
                        throw new HttpResponseException(HttpStatusCode.NotFound);
                    }

                    foreach (Session ses in sesList)
                    {
                        ses.Badge = (Int16)(ses.Badge - ses.BadgeSolicitation);
                        ses.BadgeSolicitation = 0;
                    }
                    context.SaveChanges();
                }

                return "ok";
            }
        }

        //
        // POST api/resetAcceptBadge
        //
        // Used when a client of Where? is trying to login to the System.
        //
        [ActionName("ResetAcceptBadge")]
        public string PostResetAcceptBadge([FromBody] UserEmailRequest request)
        {
            // If mail is empty return Not Found
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

                // Return not found if user is null
                if (user == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                else
                {
                    List<Session> sesList;
                    sesList = context.SessionSet
                                .Where(s => s.Active == true)
                                .Where(s => s.UserId == user.Id)
                                .ToList();

                    //The user is not logued in
                    if (sesList.Count == 0)
                    {
                        throw new HttpResponseException(HttpStatusCode.NotFound);
                    }

                    foreach (Session ses in sesList)
                    {
                        ses.Badge = (Int16)(ses.Badge - ses.BadgeAccept);
                        ses.BadgeAccept = 0;
                    }
                    context.SaveChanges();
                }

                return "ok";
            }
        }
    }
}
