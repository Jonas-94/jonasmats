using Logic.DAL;
using Logic.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Services
{
    public class LoginService
    {
        List<User> users = new List<User>();
        private InterfaceLoadSave IUser = new UserSamling();
        string filePath = "C:/Users/pc/Documents/GitHub/jonasmats/Projektuppgift/Logic/DAL/";
        string userPath = "/User.json";
        public string filePathGet { get; set; }

        public LoginService()
        {
            
        }

        public bool Login(string username, string password)
        {
            try
            {
                users = IUser.Load<User>(filePathGet);
            }
            catch { }
            return users.Exists(user => user.Username.Equals(username) && user.Password.Equals(password));
        }
    }
}
