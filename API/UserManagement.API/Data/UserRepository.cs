using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using UserManagement.API.Helper;
using UserManagement.API.Models;

namespace UserManagement.API.Data
{
    public class UserRepository : IUserRepository
    {

        public List<User> AddUser(List<User> users)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true,
                IgnoreNullValues = true,
                PropertyNameCaseInsensitive = true,
            };

            string userData = System.Text.Json.JsonSerializer.Serialize(users, options);
            File.WriteAllText(FileHelper.FILE_PATH, userData);

            /*
               1. Get the appsettings.json file
               2. Deserialize the file into ExpandoObect.
               3. If Users Key exist then update the User data else add new json data
               
            */
            string appSettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            string jsonData = File.ReadAllText(appSettingsPath);
            dynamic config = System.Text.Json.JsonSerializer.Deserialize<ExpandoObject>(jsonData, options);
            var expando = config as IDictionary<string, object>;

            if (expando.ContainsKey("Users"))
            {
                expando["Users"] = userData;

            }
            else
            {
                expando.Add("Users", userData);
            }

            string appsettingData = System.Text.Json.JsonSerializer.Serialize(config, options);
            File.WriteAllText(appSettingsPath, appsettingData);

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
                result = System.Text.Json.JsonSerializer.Deserialize<List<User>>(file, options);
            }

            return result ?? new List<User>();
        }

    }
}
