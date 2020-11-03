﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities
{
   public class Verkstad
    {
        public List<Mekaniker> mekanikerlista = new List<Mekaniker>();
        public List<Användare> användarelista = new List<Användare>();
        public List<Ärende> ärendelista = new List<Ärende>();
        Admin admin { get; set; }

        
        public void Startauppverkstad()
        {

            Skapaadmin("Bosse", "Meckarn123");
            
            Skapamekaniker("Bob", "1945", "2090", "1946");
            Skapamekaniker("hasse", "1947", "2080", "1950");
            Skapamekaniker("åke", "1930", "2045", "1932");
            Skapamekaniker("sture", "1945", "2095", "1970");

            Skapanyanvändare("Pussyslaier", "Jobdone", mekanikerlista[0]);
            Skapanyanvändare("Pussyslaier2", "Jobdone2", mekanikerlista[1]);
            Skapanyanvändare("Pussyslaier3", "Jobdone3", mekanikerlista[2]);
            Skapanyanvändare("Pussyslaier4", "Jobdone4", mekanikerlista[3]);


            
           

        }
        public void Visainfo()
        {
            for (int i = 0; i < mekanikerlista.Count; i++)
            {
                Console.WriteLine(mekanikerlista[i].Namn +" "+  mekanikerlista[i].Anställninsdatum +" "+mekanikerlista[i].Slutdatum +" "+ mekanikerlista[i].Födelsedatum
                    +" "+ mekanikerlista[i].Kbromsar);
               
            }
            Console.WriteLine("---");
        }
        public void Visaärende()
        {
            for (int i = 0; i < ärendelista.Count; i++)
            {
                if (ärendelista[i].Mekaniker != null)
                {

                 Console.WriteLine(ärendelista[i].Mekaniker.Namn);
                }
                Console.WriteLine(ärendelista[i].Avklaratärende);
            }
        }
        public void Skapanyanvändare(string lösenord, string användarnamn, Mekaniker mekaniker)
        {

            Användare nyanvändare = new Användare();
            nyanvändare.Användarnamn = användarnamn;
            nyanvändare.Lösenord = lösenord;
            nyanvändare.mekaniker = mekaniker;
            användarelista.Add(nyanvändare);

        }
        public void Skapaadmin(string användarnamn, string lösenord)
        {
            Admin nyadmin = new Admin();
            nyadmin.Användarnamn = användarnamn;
            nyadmin.Lösenord = lösenord;
            nyadmin.verkstad = this;
            
            användarelista.Add(nyadmin);
            
        }

        public void Skapamekaniker(string namn, string födelsedatum, string slutdatum, string anställningdatum)
        {
            Mekaniker mekaniker1 = new Mekaniker();
            mekaniker1.Namn = namn;
            mekaniker1.Födelsedatum = födelsedatum;
            mekaniker1.Slutdatum = slutdatum;
            mekaniker1.Anställninsdatum = anställningdatum;
            mekaniker1.Kbromsar = false;
            mekaniker1.Kdäck = false;
            mekaniker1.Kkaross = false;
            mekaniker1.Kmotor = false;
            mekaniker1.Kvindruta = false;
            mekanikerlista.Add(mekaniker1);

            
        }

    }
}
