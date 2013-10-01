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
    public class UsersController : ApiController
    {
        // This holds an IUsertRepository instance.
        // This repository holds a collection of Users to handle operations.
        // TODO: Check this http://www.asp.net/web-api/overview/extensibility/using-the-web-api-dependency-resolver
        static readonly IUserRepository repository = new UserRepository();

        //
        // GET api/users/:id
        //
        // This method name also starts with "Get", but the method has a parameter named id. 
        // This parameter is mapped to the "id" segment of the URI path. 
        // The ASP.NET Web API framework automatically converts the ID to the 
        // correct data type (int) for the parameter.
        public Users GetUser(int id)
        {
            using (var context = new DatabasePisEntities())
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
        public List<Users> GetAllUsers([FromUri] string secret_token)
        {
            if (secret_token == "abcdefgh")
            {
                using (var context = new DatabasePisEntities())
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
    }
}
