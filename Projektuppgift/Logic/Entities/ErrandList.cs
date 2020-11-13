using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities
{
    public static class ErrandList
    {
        private static List<Ärende> ärendes;

        public static List<Ärende> Ärendes
        {
            get
            {
                if (ärendes == null)
                {
                    return ärendes = new List<Ärende>();
                }
                return ärendes;
            }
            set
            {
                ärendes = value;
            }
        }
    }
}
