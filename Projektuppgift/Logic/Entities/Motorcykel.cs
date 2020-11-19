using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities
{
    public class Motorcykel:Fordon
    {
        public string ToStringBeskrivning()
        {
            return "Modellnamn: " + Modellnamn + "\n" +
                "RegNr: " + Registreringsnummer + "\n" +
                "RegDatum: " + Registreringsdatum + "\n" +
                "Milmätare: " + Milmätare + "\n" +
                "Ärende:\nBromsar: " + Äbromsar +
                "\nMotor: " + Ämotor + "\nKaross: " + Äkaross +
                "\nVindruta: " + Ävindruta + "\nDäck: " + Ädäck;
        }

    }
}
