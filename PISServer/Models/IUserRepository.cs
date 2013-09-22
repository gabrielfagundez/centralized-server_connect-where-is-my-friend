using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Note:
// This Repository Pattern allows to have multiple implementantions over
// the time. It defines getters/setters and will be implemented into 
// the class.

namespace PISServer.Models
{
    interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User Get(int id);
        User GetByEmail(string email);
        User Add(User item);
        User AddUserWithData(string mail, string name, string password, string facebookId, string linkedIn);
        void Remove(int id);
        bool Update(User item);
    }
}
