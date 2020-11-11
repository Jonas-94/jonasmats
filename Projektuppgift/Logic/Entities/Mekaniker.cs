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
        public string Namn { get; set; }
        public string Fodelsedatum { get; set; }
        public string Anstallningsdatum { get; set; }
        public string Slutdatum { get; set; }


        public bool Kbromsar { get; set; }
        public bool Kmotor { get; set; }
        public bool Kkaross { get; set; }
        public bool Kvindruta { get; set; }
        public bool Kdack { get; set; }
        
        public override string ToString()
        {
            return Namn + " " + Fodelsedatum + " " + Anstallningsdatum +" " + Slutdatum +
                " " + Kbromsar + " " + Kmotor + " " + Kkaross + " " + Kvindruta + " " + Kdack;
        }
        
        
        public Mekaniker CreateMechanic(string Namn, string Fodelsedatum, string Anstallningsdatum, string Slutdatum,
            bool bromsar, bool kaross, bool motor, bool vindruta, bool dack)
        {
            Mekaniker mekaniker = new Mekaniker();
            mekaniker.Namn = Namn;
            mekaniker.Fodelsedatum = Fodelsedatum;
            mekaniker.Anstallningsdatum = Anstallningsdatum;
            mekaniker.Slutdatum = Slutdatum;
            mekaniker.Kbromsar = bromsar;
            mekaniker.Kkaross = kaross;
            mekaniker.Kmotor = motor;
            mekaniker.Kvindruta = vindruta;
            mekaniker.Kdack = dack;
            return mekaniker;

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
                if (Kdack)
                {
                    Kdack = false;
                }
                else
                {
                    Kdack = true;
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
