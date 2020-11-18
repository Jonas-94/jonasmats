using provakod.Logic.Entities;
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
        
        public Mekaniker mekaniker;
        public string mekNamn { get; set; }
        public string fordonNamn { get; set; }
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
            Beskrivning = bromsstring + karossstring + motorstring + vindrutastring + däckstring;
            return Beskrivning;
        }
        public Ärende SkapaÄrende(string beskrivning, Fordon fordon, Mekaniker mekaniker)
        {
            this.mekNamn = mekaniker.Namn;
            this.fordonNamn = fordon.Modellnamn;
            return new Ärende
            {
                Beskrivning = beskrivning,
                fordon = fordon,
                mekaniker = mekaniker,
                ÄrendeStatus = false,
            };
        }

    }
}
