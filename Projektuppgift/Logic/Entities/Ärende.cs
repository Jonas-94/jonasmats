using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace Logic.Entities
{
    public class Ärende
    {
        
        public Fordon fordon { get; set; }

        public bool Avklaratärende = false;
        public bool Pågåendeärende = false;
        public Mekaniker Mekaniker;

        public void Sparaärende()
        {
            List<Ärende> Ärendelista = new List<Ärende>();
            Ärendelista.Add(this);
            Ärendelista.Add(this);
            string json = JsonConvert.SerializeObject(Ärendelista.ToArray());
            System.IO.File.WriteAllText(@"C: \Users\Acer\OneDrive\Dokument\Repository\C - Sharp\Projektuppgift\Ärenden.json", json);
        }

        internal void Utförärende()
        {
            Pågåendeärende = false;
            Avklaratärende = true;
        }

        public void Setavklaratärende(bool avklaratärende)
        {
            Avklaratärende = avklaratärende;
            Sparaärende();
        }
        public void Setpågåendeärende(bool pågåendeärende)
        {
            Pågåendeärende = pågåendeärende;
            Sparaärende();
        }

        public void Bytamekaniker(Mekaniker nymekaniker)
        {
            Mekaniker = nymekaniker;
            Sparaärende();
        }
    }
}
