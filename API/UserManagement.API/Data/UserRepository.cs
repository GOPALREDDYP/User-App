
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using UserManagement.API.Helper;
using UserManagement.API.Models;

namespace UserManagement.API.Data
{
    public class UserRepository : IUserRepository
    {

        public List<User> AddUser(List<User> users)
        {
            string userData = JsonSerializer.Serialize(users);
            File.WriteAllText(FileHelper.FILE_PATH, userData);
            return GetUsers();
        }

        public List<User> GetUsers()
        {
            List<User> result = null;

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
                IgnoreNullValues = true,
                PropertyNameCaseInsensitive = true,
            };

            if (File.Exists(FileHelper.FILE_PATH))
            {
                string file = File.ReadAllText(FileHelper.FILE_PATH);
                result = JsonSerializer.Deserialize<List<User>>(file, options);
            }

            return result ?? new List<User>();
        }

    }
}
