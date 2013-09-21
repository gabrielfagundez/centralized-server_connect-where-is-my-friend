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
        // GET api/users 
        //
        // The method name starts with "Get", so by convention it maps to GET requests. 
        // Also, because the method has no parameters, it maps to a URI that does 
        // not contain an "id" segment in the path.
        public IEnumerable<User> GetAllUsers()
        {
            return repository.GetAll();
        }

        //
        // GET api/users/:id
        //
        // This method name also starts with "Get", but the method has a parameter named id. 
        // This parameter is mapped to the "id" segment of the URI path. 
        // The ASP.NET Web API framework automatically converts the ID to the 
        // correct data type (int) for the parameter.
        public User GetUser(int id)
        {
            User item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        //
        // POST api/users
        //
        public HttpResponseMessage PostUser(User item)
        {
            item = repository.Add(item);
            var response = Request.CreateResponse<User>(HttpStatusCode.Created, item);

            string uri = Url.Link("DefaultApi", new { id = item.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        //
        // PUT api/users/:id
        //
        public void PutUser(int id, User user)
        {
            user.Id = id;
            if (!repository.Update(user))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        //
        // DELETE api/users/5
        //
        // @param [Int] id This indicates the id of the user.
        public void DeleteUser(int id)
        {
            User item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            repository.Remove(id);
        }
    }
}
