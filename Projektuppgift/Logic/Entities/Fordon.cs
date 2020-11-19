
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities
{
    public abstract class Fordon
    {
        public int Id { get; set; }
        public string Modellnamn { get; set; }
        public string Registreringsnummer { get; set; }
        public decimal Milmätare { get; set; }
        public string Registreringsdatum { get; set; }
        public string Drivmedel { get; set; }

        public bool Äbromsar { get; set; }
        public bool Ämotor { get; set; }
        public bool Äkaross { get; set; }
        public bool Ävindruta { get; set; }
        public bool Ädäck { get; set; }

    }
}
