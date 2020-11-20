using Logic.DAL;
using Logic.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Logic.Services
{
    public class LoginService
    {
        FileLoader fLoader = new FileLoader();
        List<User> users = new List<User>();
        private InterfaceLoadSave IUser = new UserSamling();
        string filePath = "C:/Users/pc/Documents/GitHub/jonasmats/Projektuppgift/Logic/DAL/";
        string userPath = "/User.json";
        public string filePathGet { get; set; } //= "C:/Users/pc/Documents/GitHub/jonasmats/Projektuppgift/Logic/DAL/User.json";

        public LoginService()
        {
            
        }
        public bool Login(string username, string password)
        {
            fLoader.LoadUsers();
            IEnumerable<User> findID = fLoader.userSamling.users.Where(user => user.Username.Equals(username) && user.Password.Equals(password));
            foreach (var x in findID)
                fLoader.Id = x.ID;
            return fLoader.userSamling.users.Exists(user => user.Username.Equals(username) && user.Password.Equals(password));
        }
    }
}
