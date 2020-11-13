using System;
using System.Collections.Generic;
using System.Text;

namespace provakod.Logic.Entities
{
    public static class ID
    {
        public static int Fid = 0, Mid = 0, Äid = 0;
        
        public static int Fnästaid()
        {
            Fid += 1;
            return Fid;
        }
        public static int Mnästaid()
        {
            Mid += 1;
            return Mid;
        }
        public static int Änästaid()
        {
            Äid += 1;
            return Äid;
        }
    }
}
