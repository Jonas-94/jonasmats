using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities
{
    class Admin:Användare
    {
        public List<Ärende> ärendelista = new List<Ärende>();
        public List<Användare> användarlista = new List<Användare>();
        public List<Mekaniker> mekanikerlista = new List<Mekaniker>();


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
            mekanikerlista.Add(mekaniker);
        }
        public void Tabortmekaniker(string namn)
        {
            for (int i = 0; i < mekanikerlista.Count; i++)
            {
                if (mekanikerlista[i].Namn == namn)
                {
                    mekanikerlista.Remove(mekanikerlista[i]);
                }
            }
        }
        public void Läggtillkpmpetens(string kompetens, string namn)
        {
            for (int i = 0; i < mekanikerlista.Count; i++)
            {
                if (mekanikerlista[i].Namn == namn)
                {
                    mekanikerlista[i].Ändrakompetens(kompetens);
                    // den mekanikern som vi väljer i listan ändrar vi kompetens på.
                }
            }
        }
        public void Mekanikeriärende(Ärende ärende, string namn)
        {
            Mekaniker mekaniker;
            for (int i = 0; i < mekanikerlista.Count; i++)
            {

                if (mekanikerlista[i].Namn == namn)
                {
                    mekaniker = mekanikerlista[i];
                    if (mekaniker.Getaktivaärenden() < 2)
                    {
                        ErrandList.Ärendes.Add(ärende);
                    }

                }

            }

        }
        public void Läggtillärenden(Ärende ärende)
        {
            ErrandList.Ärendes.Add(ärende);
            
        }
        public void Utförärende(Ärende ärende)
        {
            ärende.Utförärende();
        }
    }
}
