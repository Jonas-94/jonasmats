using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities
{
    public abstract class Fordon
    {
        public string Modellnamn { get; set; }
        public string Registreringsnummer { get; set; }
        public decimal Milmätare { get; set; }
        public string Registreringsdatum { get; set; }
        public string Drivmedel { get; set; }
        public int Däck { get; set; }
    }
}
