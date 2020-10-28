using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities
{
    class Verkstad
    {
        List<Mekaniker> mekanikerlista = new List<Mekaniker>();
        List<Användare> användarelista = new List<Användare>();
        List<Ärende> ärendelista = new List<Ärende>();

        public void Startauppverkstad()
        {
            Admin bosse = new Admin();
            bosse.Användarnamn = "Bosse";

        }

        public void Skapanyanvändare(string lösenord, string användarnamn, Mekaniker mekaniker)
        {
            Användare nyanvändare = new Användare();
            nyanvändare.Användarnamn = användarnamn;
            nyanvändare.Lösenord = lösenord;
            nyanvändare.mekaniker = mekaniker;
        }
        public void Skapaadmin(string användarnamn, string lösenord, List<Ärende> ärendelista, List<Användare> användarelista, List<Mekaniker> mekanikerlista)
        {
            Admin nyadmin = new Admin();
            nyadmin.Användarnamn = användarnamn;
            nyadmin.Lösenord = lösenord;

        }
    }
}
