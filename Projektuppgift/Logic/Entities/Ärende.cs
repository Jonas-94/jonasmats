
using System;
using System.Collections.Generic;
//using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace Logic.Entities
{
    public class Ärende
    {

        public Fordon fordon;
        public string Beskrivning { get; set; }
        
        public bool ÄrendeStatus { get; set; }
        public int ÄrendeID { get; set; }
        
        public Mekaniker mekaniker { get; set; }
        public string förnamn { get; set; }
        public string efternamn { get; set; }
        public string RegNr { get; set; }
        public string RegDatum { get; set; }

       

        public string BeskrivningsMetod(Fordon fordon)
        {
            bool broms = fordon.Äbromsar;
            bool kaross = fordon.Äkaross;
            bool motor = fordon.Ämotor;
            bool vindruta = fordon.Ävindruta;
            bool däck = fordon.Ädäck;
            string bromsstring="", karossstring="", motorstring="", vindrutastring="", däckstring="";
            if (broms == true)
                bromsstring = "\nBehandling av bromsar";
            if (kaross == true)
                karossstring = "\nBehanding av kaross";
            if (motor == true)
                motorstring = "\nBehandling av motor";
            if (vindruta == true)
                vindrutastring = "\nBehandling av vindruta";
            if (däck == true)
                däckstring = "\nBehandling av däck";
            Beskrivning = " " + bromsstring + karossstring + motorstring + vindrutastring + däckstring;
            return Beskrivning;
        }
    }
}
