using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using PISServer.Models;
using PisDataAccess;


namespace PISServer.Controllers
{
    public class FriendsController : ApiController
    {
        [HttpPost]
        public string AddFriend()
        {
            // Tiene que devolver la informacion del segundo usuario (no la pass!)
            //Error: si no existe un id 404
            //Error: si no existe un id 410 gone
            using (var context = new DevelopmentPISEntities())
            {
                return "Method Not Implemented";
            }
        }

        [HttpGet]
        public List<TempUserClass> GetAllFriends(int id)
        {
            using (var context = new DevelopmentPISEntities())
            {
                List<TempUserClass> dinosaurs = new List<TempUserClass>();

                TempUserClass u1 = new TempUserClass { Id = 145, FacebookId = "111", LinkedInId = "aaa", Mail = "testingmail1@google.com", Name = "Mario" };
                TempUserClass u2 = new TempUserClass { Id = 178, FacebookId = "222", LinkedInId = "bbb", Mail = "testingmail2@google.com", Name = "Pipo" };
                TempUserClass u3 = new TempUserClass { Id = 230, FacebookId = "444", LinkedInId = "ccc", Mail = "testingmail3@google.com", Name = "Maestro" };

                dinosaurs.Add(u1);
                dinosaurs.Add(u2);
                dinosaurs.Add(u3);

                return dinosaurs;
            }
        }
    }
}
