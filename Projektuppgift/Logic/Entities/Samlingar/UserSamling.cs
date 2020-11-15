using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.IO;
using Logic.DAL;

namespace Logic.Entities
{
    public class UserSamling : InterfaceLoadSave
    {
        public List<User> users { get; set; } = new List<User>();
        
        public override string ToString()
        {
            List<User> users = new List<User>();
            StringBuilder sb = new StringBuilder("mekaniker:[");
            for (int i = 0; i < users.Count; i++)
            {
                sb.Append(users[i].ToString());
                sb.Append(",");
            }
            sb.Append("]");
            return sb.ToString();

        }
        void InterfaceLoadSave.Save(string filePath)
        {
            System.IO.File.WriteAllText(filePath, "");
            string json = JsonSerializer.Serialize(this);
            FileStream fs = File.OpenWrite(filePath);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(json);
            sw.Close();
        }
        List<T> InterfaceLoadSave.Load<T>(string filePath)
        {
            FileStream fs = File.OpenRead(filePath);
            StreamReader sr = new StreamReader(fs);
            string json = sr.ReadToEnd();
            UserSamling user = JsonSerializer.Deserialize<UserSamling>(json);
            sr.Close();
            return user.users as List<T>;
        }
    }
}
