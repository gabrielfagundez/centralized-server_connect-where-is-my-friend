using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PISServer.Models
{
    public class UserRepository
    {
        private List<User> users = new List<User>();
        private int _nextId = 1;

        public UserRepository()
        {
            Add(new User { Name = "User 1", Password = "Pass1" });
            Add(new User { Name = "User 2", Password = "Pass2" });
            Add(new User { Name = "User 3", Password = "Pass3" });
        }

        public IEnumerable<User> GetAll()
        {
            return users;
        }

        public User Get(int id)
        {
            return users.Find(p => p.Id == id);
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