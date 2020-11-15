using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities
{
    public class Användare
    {
        public string Användarnamn { get; set; }
        public int Mekanikerid { get; set; }
        public string Lösenord { get; set; }
        public Mekaniker mekaniker;

        /*
        public void Startaden(Mekaniker mekanikerm)
        {
            Mekanikerid = mekanikerm.Id;
            mekaniker = mekanikerm;

        }

        //en metod som gör att mekanikeranvändaren kan utföra ett ärende.
        public void Utförärende(Ärende ärende)
        {
            ärende.Utförärende();
        }

        public void Fixakompetens(string kompetens)
        {

            mekaniker.Ändrakompetens(kompetens);
        }

        */



    }
}
