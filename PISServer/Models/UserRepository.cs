using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// Note:
// This Repository Pattern allows to have multiple implementantions over
// the time. It defines getters/setters and will be implemented into 
// the class.

namespace PISServer.Models
{
    public class UserRepository : IUserRepository
    {
        private List<User> users = new List<User>();
        private int _nextId = 1;

        public UserRepository()
        {
            Add(new User { Id = 1001, Email = "gabriel.fa07@gmail.com", FacebookId = "568349440", Password = "pass" });
            Add(new User { Id = 1002, Email = "fab.kremer@gmail.com", FacebookId = "568349440", Password = "pass" });
            Add(new User { Id = 1003, Email = "felipepuig@gmail.com", FacebookId = "568349440", Password = "pass" });
            Add(new User { Id = 1004, Email = "gabriel@mordecki.com", FacebookId = "568349440", Password = "pass" });
            Add(new User { Id = 1005, Email = "ignacio.villaverde35@gmail.com", FacebookId = "568349440", Password = "pass" });
            Add(new User { Id = 1006, Email = "jivarela26@gmail.com", FacebookId = "568349440", Password = "pass" });
            Add(new User { Id = 1007, Email = "renzomassobrio@gmail.com", FacebookId = "568349440", Password = "pass" });
            Add(new User { Id = 1008, Email = "vicefava@hotmail.com", FacebookId = "568349440", Password = "pass" });
            Add(new User { Id = 1009, Email = "guidufort@msn.com", FacebookId = "568349440", Password = "pass" });
            Add(new User { Id = 1010, Email = "maitingui@gmail.com", FacebookId = "568349440", Password = "pass" });
            Add(new User { Id = 1011, Email = "fedemz88@gmail.com", FacebookId = "568349440", Password = "pass" });
        }

        public IEnumerable<User> GetAll()
        {
            return users;
        }

        public User Get(int id)
        {
            return users.Find(p => p.Id == id);
        }

        // Get a user given an email.
        public User GetByEmail(string email)
        {
            return users.Find(user => user.Email == email);
        }
        
        public User Add(User item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("UserNotFound");
            }
            item.Id = _nextId++;
            users.Add(item);
            return item;
        }

        public void Remove(int id)
        {
            users.RemoveAll(p => p.Id == id);
        }

        public bool Update(User item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("UserNotFound");
            }
            int index = users.FindIndex(p => p.Id == item.Id);
            if (index == -1)
            {
                return false;
            }
            users.RemoveAt(index);
            users.Add(item);
            return true;
        }
    }
}