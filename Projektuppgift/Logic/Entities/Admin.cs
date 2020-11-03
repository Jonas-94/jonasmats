using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities
{
    class Admin:Användare
    {
        
        public Verkstad verkstad;

        
        public void Läggatillmekaniker(string namn, string födelsedatum, string anställningsdatum, string slutdatum)
        {
            Mekaniker mekaniker = new Mekaniker();
            mekaniker.Namn = namn;
            mekaniker.Födelsedatum = födelsedatum;
            mekaniker.Anställninsdatum = anställningsdatum;
            mekaniker.Slutdatum = slutdatum;
            mekaniker.Kbromsar = false;
            mekaniker.Kdäck = false;
            mekaniker.Kkaross = false;
            mekaniker.Kmotor = false;
            mekaniker.Kvindruta = false;
            verkstad.mekanikerlista.Add(mekaniker);
        }
        public void Tabortmekaniker(string namn)
        {
            for (int i = 0; i < verkstad.mekanikerlista.Count; i++)
            {
                if (verkstad.mekanikerlista[i].Namn == namn)
                {
                    verkstad.mekanikerlista.Remove(verkstad.mekanikerlista[i]);
                }
            }
        }
        public void Läggtillkpmpetens(string kompetens, string namn)
        {
            for (int i = 0; i < verkstad.mekanikerlista.Count; i++)
            {
                if (verkstad.mekanikerlista[i].Namn == namn)
                {
                    verkstad.mekanikerlista[i].Ändrakompetens(kompetens);
                    // den mekanikern som vi väljer i listan ändrar vi kompetens på.
                }
            }
        }
        public void Mekanikeriärende(Ärende ärende, string namn)
        {
            Mekaniker mekaniker;
            for (int i = 0; i < verkstad.mekanikerlista.Count; i++)
            {

                if (verkstad.mekanikerlista[i].Namn == namn)
                {
                    mekaniker = verkstad.mekanikerlista[i];
                    if (mekaniker.Getaktivaärenden() < 2)
                    {
                        mekaniker.märendelista.Add(ärende);
                        ärende.Mekaniker = mekaniker;
                        ärende.Pågåendeärende = true;
                    }

                }

            }

        }
        public void Läggtillärenden(Ärende ärende)
        {
            verkstad.ärendelista.Add(ärende);
            
        }
        public void Utförtärende(Ärende ärende)
        {
            ärende.Utförärende();
        }
    }
}
