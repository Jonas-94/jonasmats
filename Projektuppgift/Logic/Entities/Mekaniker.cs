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
        public string Födelsedatum { get; set; }
        public string Anställninsdatum { get; set; }
        public string Slutdatum { get; set; }


        public bool Kbromsar { get; set; }
        public bool Kmotor { get; set; }
        public bool Kkaross { get; set; }
        public bool Kvindruta { get; set; }
        public bool Kdäck { get; set; }

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
