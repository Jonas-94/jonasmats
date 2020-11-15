using Logic.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;

namespace provakod.Logic.DAL
{/*
    class Laddainjson: Interface_ladda_spara_json
    {
        
        public string path { get; set; }

        List<Ärende> ärendelista = new List<Ärende>();
        List<Fordon> fordonslista = new List<Fordon>();
        List<Användare> användarlista = new List<Användare>();
        List<Mekaniker> meklista = new List<Mekaniker>();
        public void Laddajson()
        {
            ärendelista = new List<Ärende>();
            fordonslista = new List<Fordon>();
            
            string mekaniker = File.ReadAllText(path + "Mekaniker.Json");
            meklista = JsonSerializer.Deserialize<List<Mekaniker>>(mekaniker);
            foreach (var m in meklista)
            {
                Console.WriteLine(m);
            }
            string användare = File.ReadAllText(path + "Användare.Json");
            användarlista = JsonSerializer.Deserialize<List<Användare>>(användare);

            string Bil = File.ReadAllText(path + "Bil.json");
            List<Bil> billista = JsonSerializer.Deserialize<List<Bil>>(Bil);

            string Buss = File.ReadAllText(path + "Buss.json");
            List<Buss> busslista = JsonSerializer.Deserialize<List<Buss>>(Buss);

            string Motorcykel = File.ReadAllText(path + "Motorcykel.json");
            List<Motorcykel> motorcykellista = JsonSerializer.Deserialize<List<Motorcykel>>(Motorcykel);

            string Lastbil = File.ReadAllText(path + "Lastbil.json");
            List<Lastbil> lastbilslista = JsonSerializer.Deserialize<List<Lastbil>>(Lastbil);


            string ärendebroms = File.ReadAllText(path + "Bromsar.json");
            List<Bromsar> ärendelistabroms = JsonSerializer.Deserialize<List<Bromsar>>(ärendebroms);

            string ärendevindruta = File.ReadAllText(path + "Vindruta.json");
            List<Vindruta> ärendelistavindruta = JsonSerializer.Deserialize<List<Vindruta>>(ärendevindruta);

            string ärendedäck = File.ReadAllText(path + "Däck.json");
            List<Däck> ärendelistadäck = JsonSerializer.Deserialize<List<Däck>>(ärendedäck);

            string ärendekaross = File.ReadAllText(path + "Kaross.json");
            List<Kaross> ärendelistakaross = JsonSerializer.Deserialize<List<Kaross>>(ärendekaross);

            string ärendemotor = File.ReadAllText(path + "Motor.json");
            List<Motor> ärendelistamotor = JsonSerializer.Deserialize<List<Motor>>(ärendemotor);

            ärendelistabroms.ForEach(ä => ärendelista.Add(ä));
            ärendelistavindruta.ForEach(ä => ärendelista.Add(ä));
            ärendelistadäck.ForEach(ä => ärendelista.Add(ä));
            ärendelistakaross.ForEach(ä => ärendelista.Add(ä));
            ärendelistamotor.ForEach(ä => ärendelista.Add(ä));

            billista.ForEach(f => fordonslista.Add(f));
            lastbilslista.ForEach(f => fordonslista.Add(f));
            motorcykellista.ForEach(f => fordonslista.Add(f));
            busslista.ForEach(f => fordonslista.Add(f));

            foreach (var ä in ärendelista)
            {
                foreach (var f in fordonslista)
                {
                    if (ä.Fid == f.Id)
                    {
                        ä.fordon = f;

                    }
                }
            }
            foreach ( var m in meklista)
            {
                foreach ( var i in m.idlista)
                {
                    foreach (var ä in ärendelista)
                    {
                        if (ä.Id == i)
                        {
                            m.märendelista.Add(ä);
                            ä.Mekaniker = m;
                        }
                    }
                }
            }
            foreach (var a in användarlista)
            {
                foreach (var m in meklista)
                {
                    if (m.Id == a.Mekanikerid)
                    {
                        a.mekaniker = m;
                    }
                }
            }
            
        }

        public List<Användare> Getanvändare()
        {
            

            return användarlista;
        }

       public List<Mekaniker> Getmekaniker()
        {
           
            return meklista;
        }

        public List<Ärende> Getärende()
        {
           
            return ärendelista;
        }
        
    }*/
}
