using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities
{
    public class Samlingar
    {
        public List<Mekaniker> mekaniker = new List<Mekaniker>();
        public string AppendToString<T>(List <T>list)
        {
            StringBuilder sb = new StringBuilder("[");
            for (int i = 0; i < list.Count; i++)
            {
                sb.Append(list[i].ToString());
                sb.Append(",");
            }
            sb.Append("]");
            return sb.ToString();
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("[");
            for (int i = 0; i < mekaniker.Count; i++)
            {
                sb.Append(mekaniker[i].ToString());
                sb.Append(",");
            }
            sb.Append("]");
            return sb.ToString();

        }
    }
}
