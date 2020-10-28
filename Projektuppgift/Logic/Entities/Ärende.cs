using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace Logic.Entities
{
    abstract class Ärende
    {
        Fordon fordon { get; set; }

        public bool Avklaratärende;
        public bool Pågåendeärende;
        public Mekaniker Mekaniker;

        public abstract void Utförärende();

        public void Sparaärende()
        {
            List<Ärende> Ärendelista = new List<Ärende>();
            Ärendelista.Add(this);
            string json = JsonConvert.SerializeObject(Ärendelista.ToArray());
            System.IO.File.WriteAllText(@"C: \Users\Acer\OneDrive\Dokument\Repository\C - Sharp\Projektuppgift\Ärenden.json", json);
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
