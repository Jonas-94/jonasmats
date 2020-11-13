using provakod.Logic.Entities;
using System;
using System.Collections.Generic;
//using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace Logic.Entities
{
    public class Ärende
    {

        public Fordon fordon;
        public int Id { get; set; }
        public int Fid { get; set; }
        public string Beskrivning { get; set; }

        public bool Avklaratärende { get; set; }
        public bool Pågåendeärende { get; set; }
        public Mekaniker Mekaniker;

        public void UppdateraFid()
        {
            Fid = fordon.Id;
        }
        public Ärende()
        {
            Avklaratärende = false;
            Pågåendeärende = false;
            
            this.Id = ID.Änästaid();
            
        }

        public void Startaupp(Fordon fordonm)
        {
            fordon = fordonm;
            Fid = fordonm.Id;
        }
        

        public void Sparaärende()
        {
            //List<Ärende> Ärendelista = new List<Ärende>();
            //Ärendelista.Add(this);
            //ErrandList.Ärendes.Add(this);
            //string json = JsonConvert.SerializeObject(ErrandList.Ärendes.ToArray());
            //System.IO.File.WriteAllText(@"C: \Users\Acer\OneDrive\Dokument\Repository\C - Sharp\Projektuppgift\Ärenden.json", json);
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
