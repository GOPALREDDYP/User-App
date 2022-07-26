using System.Collections.Generic;
using UserManagement.API.Dtos;

namespace UserManagement.API.BusinessProcess
{
    public interface IUserBP
    {
        List<UserResultDto> GetUsers();

        List<UserResultDto> AddUser(string firstName, string lastName);
    }
}
