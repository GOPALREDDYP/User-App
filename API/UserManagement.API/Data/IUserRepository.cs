using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.API.Models;

namespace UserManagement.API.Data
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        List<User> AddUser(List<User> users);
    }
}
