using provakod.Logic.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Logic.Entities
{
    public class Mekaniker//: INotifyPropertyChanged
    {
        //  public event PropertyChangedEventHandler PropertyChanged;

        public List<Ärende> märendelista = new List<Ärende>();
        public List<int> idlista { get; set; }
        
        public int Id { get; set; }
        public string Namn { get; set; }
        public string Födelsedatum { get; set; }
        public string Anställningsdatum { get; set; }
        public string Slutdatum { get; set; }


        public bool Kbromsar { get; set; }
        public bool Kmotor { get; set; }
        public bool Kkaross { get; set; }
        public bool Kvindruta { get; set; }
        public bool Kdäck { get; set; }

        public override string ToString()
        {
            return Namn + " " + Födelsedatum + " " + Anställningsdatum + " " + Slutdatum +
                " " + Kbromsar + " " + Kmotor + " " + Kkaross + " " + Kvindruta + " " + Kdäck;
        }


        public Mekaniker CreateMechanic(string Namn, string Fodelsedatum, string Anstallningsdatum, string Slutdatum,
            bool bromsar, bool kaross, bool motor, bool vindruta, bool dack)
        {
            Mekaniker mekaniker = new Mekaniker();
            mekaniker.Namn = Namn;
            mekaniker.Födelsedatum = Fodelsedatum;
            mekaniker.Anställningsdatum = Anstallningsdatum;
            mekaniker.Slutdatum = Slutdatum;
            mekaniker.Kbromsar = bromsar;
            mekaniker.Kkaross = kaross;
            mekaniker.Kmotor = motor;
            mekaniker.Kvindruta = vindruta;
            mekaniker.Kdäck = dack;
            return mekaniker;

        }
        public Mekaniker()
        {
            
           
            this.Id = ID.Mnästaid();
            idlista = new List<int>();
            
        }

        public void Läggtillid()
        {
            idlista = new List<int>();
            foreach ( var i in märendelista)
            {
                idlista.Add(i.Id);
            }
        }

        public void Ändrakompetens(string kompetens)
        {

            if (kompetens == "Bromsar")
            {
                if (Kbromsar)
                {
                    Kbromsar = false;
                }
                else
                {
                    Kbromsar = true;
                }
            }

            else if (kompetens == "Motor")
            {
                if (Kmotor)
                {
                    Kmotor = false;
                }
                else
                {
                    Kmotor = true;
                }
            }
            else if (kompetens == "Kaross")
            {
                if (Kkaross)
                {
                    Kkaross = false;
                }
                else
                {
                    Kkaross = true;
                }
            }
            else if (kompetens == "Vindruta")
            {
                if (Kvindruta)
                {
                    Kvindruta = false;
                }
                else
                {
                    Kvindruta = true;
                }
            }
            else if (kompetens == "Däck")
            {
                if (Kdäck)
                {
                    Kdäck = false;
                }
                else
                {
                    Kdäck = true;
                }
            }

        }
        public int Getaktivaärenden()
        {
            int sumärende = 0;
            if (märendelista != null)
            {
                foreach (var ä in märendelista)
                {
                    if (ä.Avklaratärende == false)
                    {
                        sumärende += 1;
                    }
                }
            }
           
            return sumärende;
        }

        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        //protected void OnPropertyChanged([CallerMemberName] string name = "")
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        //}
    }
}
