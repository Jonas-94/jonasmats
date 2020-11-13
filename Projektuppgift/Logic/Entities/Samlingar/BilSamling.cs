using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities
{
    public class BilSamling
    {
        public List<Bil> Bilar { get; set; } = new List<Bil>();
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("[");

            for (int i = 0; i < Bilar.Count; i++)
            {
                sb.Append(Bilar[i].ToString());
                sb.Append(",");
            }
            sb.Append("]");

            return sb.ToString();

        }
    }
}
