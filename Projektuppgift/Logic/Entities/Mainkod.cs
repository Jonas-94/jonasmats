using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Logic.Entities
{
    public class Mainkod
    {
       
        static Verkstad verkstad = new Verkstad();
        static void Main(string[] args)
        {

            verkstad.Startauppverkstad();
            Admin admin = (Admin)verkstad.användarelista[0];
            verkstad.Visainfo();
            admin.Läggatillmekaniker("Johsn", "bsfs", "sfgs", "gsdgsdf");

            verkstad.Visainfo();

        }
    }
}
