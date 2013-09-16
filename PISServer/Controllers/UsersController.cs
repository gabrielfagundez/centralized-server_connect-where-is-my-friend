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
        // 
        // GET api/users 
        // Returns a list of users from the database
        //
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //
        // GET api/users/5
        // Returns a json of a user from a given id
        //
        public string Get(int id)
        {
            return "value";
        }

        //
        // POST api/users
        // ?
        //
        public void Post([FromBody]string value)
        {
        }

        //
        // PUT api/users/5
        // ?
        //
        public void Put(int id, [FromBody]string value)
        {
        }

        //
        // DELETE api/users/5
        // Delete a user from a given id
        //
        public void Delete(int id)
        {
        }
    }
}
