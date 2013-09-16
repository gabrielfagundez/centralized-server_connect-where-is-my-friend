using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PISServer.Controllers
{
    public class UsersController : ApiController
    {
        // This holds an IProductRepository instance.
        // This repository holds a collection of Users to handle operations.
        static readonly PISServer.Models.IUserRepository repository = new PISServer.Models.UserRepository();

        // 
        // GET api/users 
        //
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //
        // GET api/users/5
        //
        public string Get(int id)
        {
            return "value";
        }

        //
        // POST api/users
        //
        public void Post([FromBody]string value)
        {
        }

        //
        // PUT api/users/5
        //
        public void Put(int id, [FromBody]string value)
        {
        }

        //
        // DELETE api/users/5
        //
        public void Delete(int id)
        {
        }
    }
}
