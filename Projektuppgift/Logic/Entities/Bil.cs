using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities
{
    public class Bil : Fordon
    {
        public string bilTyp { get; set; }
        public bool Dragkrok { get; set; }
        public string ToStringBeskrivning()
        {
            return "Modellnamn: " + Modellnamn + "\n" +
                "Biltyp: " + bilTyp + "\n" +
                "Dragkrok" + Dragkrok + "\n" +
                "RegNr: " + Registreringsnummer + "\n" +
                "RegDatum: " + Registreringsdatum + "\n" +
                "Milmätare: " + Milmätare + "\n" +
                "Ärende:\nBromsar: " + Äbromsar +
                "\nMotor: " + Ämotor + "\nKaross: " + Äkaross +
                "\nVindruta: " + Ävindruta + "\nDäck: " + Ädäck;
        }

    }
}
