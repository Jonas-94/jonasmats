using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities
{
    public class MekanikerSamling
    {
        public List<Mekaniker> mekaniker { get; set; } = new List<Mekaniker>();
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("mekaniker:[");
            for (int i = 0; i < mekaniker.Count;i++)
            {
                sb.Append(mekaniker[i].ToString());
                sb.Append(",");
            }
            sb.Append("]");
            return sb.ToString();

        }
        
    }
}
