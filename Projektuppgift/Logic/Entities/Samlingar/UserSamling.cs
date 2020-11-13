using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities
{
    public class UserSamling
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
    }
}
