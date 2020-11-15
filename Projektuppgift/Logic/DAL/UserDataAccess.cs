using Logic.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Logic.DAL
{
    public class UserDataAccess
    {
        private const string path = @"DAL\User.json";

        /// <summary>
        /// https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-how-to
        /// </summary>
        /// <returns></returns>
        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            FileStream fs = File.OpenRead(path);
            StreamReader sr = new StreamReader(fs);
            string json = sr.ReadToEnd();
            UserSamling us = JsonSerializer.Deserialize<UserSamling>(json);
            us = JsonSerializer.Deserialize<UserSamling>(json);

            foreach (var u in us.users)
                users.Add(u);

            return users;
        }
    }
}
