
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
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

                //Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.All),
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true,
                IgnoreNullValues = true,
                PropertyNameCaseInsensitive = true,
            };

            string userData = System.Text.Json.JsonSerializer.Serialize(users, options);
            File.WriteAllText(FileHelper.FILE_PATH, userData);

            var appSettingsPath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "appsettings.json");
            var json = File.ReadAllText(appSettingsPath);

            var jsonSettings = new JsonSerializerSettings();
            jsonSettings.Converters.Add(new ExpandoObjectConverter());
            jsonSettings.Converters.Add(new StringEnumConverter());

          //  dynamic config = JsonConvert.DeserializeObject<ExpandoObject>(json, jsonSettings);

            dynamic config = System.Text.Json.JsonSerializer.Deserialize<ExpandoObject>(json, options);

            var expando = config as IDictionary<string, object>;
            //expando.Add("users2", new User() { FirstName = "Gopal POST2", LastName = "Reddy POST2" });

            if (expando.ContainsKey("Users"))
            {
                // IDictionary<string, object> rating = expando["Users"] as Dictionary<string, object>;
                // List<User> result = new List<User>();
                // dynamic result2 = JsonConvert.DeserializeObject<ExpandoObject>(expando["Users"].ToString(), jsonSettings);

                // result.AddRange(users);
                //  string userslist = System.Text.Json.JsonSerializer.Serialize(result);
                //  expando["Users"] = userslist;

                //var userJson = JsonConvert.SerializeObject(expando["Users"]);
                //List<User> result = System.Text.Json.JsonSerializer.Deserialize<List<User>>(userJson, options);

                //if (result.Any())
                //{
                //    result.AddRange(users);
                //}

                // var rs  =JsonConvert.SerializeObject<User>(result);

                // string userSrelializerData = System.Text.Json.JsonSerializer.Serialize(users, options);
                expando["Users"] = userData;

                // string appsettingData = System.Text.Json.JsonSerializer.Serialize(config, options);

                //  appsettingData = System.Text.Json.JsonSerializer.Serialize(expando);

                //string userData = System.Text.Json.JsonSerializer.Serialize(users);
               // File.WriteAllText(appSettingsPath, appsettingData);

            }
            else
            {
                expando.Add("Users", userData);
            }

            string appsettingData = System.Text.Json.JsonSerializer.Serialize(config, options);
            File.WriteAllText(appSettingsPath, appsettingData);

            //string userData = System.Text.Json.JsonSerializer.Serialize(users, options);
            //File.WriteAllText(FileHelper.FILE_PATH, userData);


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
