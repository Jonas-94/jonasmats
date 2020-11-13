using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities
{
    class AnvändarSamling
    {
        public List<Användare> användare { get; set; } = new List<Användare>();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("[");

            for (int i = 0; i < användare.Count; i++)
            {
                sb.Append(användare[i].ToString());
                sb.Append(",");
            }
            sb.Append("]");

            return sb.ToString();

        }
    }
}

