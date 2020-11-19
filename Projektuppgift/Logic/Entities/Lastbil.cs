using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities
{
    public class Lastbil:Fordon
    {
        public decimal Maxvikt { get; set; }

        
            public string ToStringBeskrivning()
            {
                return "Modellnamn: " + Modellnamn + "\n" +
                    "RegNr: " + Registreringsnummer + "\n" +
                    "RegDatum: " + Registreringsdatum + "\n" +
                    "Milmätare: " + Milmätare + "\n" +
                    "Maxvikt: " + Maxvikt + "\n" +
                    "Ärende:\nBromsar: " + Äbromsar +
                    "\nMotor: " + Ämotor + "\nKaross: " + Äkaross +
                    "\nVindruta: " + Ävindruta + "\nDäck: " + Ädäck;
            }
        
    }
    
}
