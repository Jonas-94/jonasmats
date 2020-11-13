using Logic.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Linq;

namespace provakod.Logic.DAL
{
    public class ijsonmekaniker: Interface_ladda_spara_json

    {
        public string path { get; set; }
    public void Sparajson(Verkstad verkstad)
    {
            
            
            List<Fordon> fordonslista = new List<Fordon>();
            foreach (var f in verkstad.ärendelista)
            {
                fordonslista.Add(f.fordon);
                f.UppdateraFid();

            }
            string json = JsonSerializer.Serialize(verkstad.användarelista[0]);
            File.WriteAllText(path + "Admin.json", json);

            json = JsonSerializer.Serialize(Returnera(verkstad.användarelista));
            File.WriteAllText(path + "Användare.json", json);

            json = JsonSerializer.Serialize(verkstad.ärendelista);
            File.WriteAllText(path + "Ärende.json", json);

            Returnlist(fordonslista);
            Returnärende(verkstad.ärendelista);



            foreach (var i in verkstad.mekanikerlista)
            {
                i.Läggtillid();
            }
            json = JsonSerializer.Serialize(verkstad.mekanikerlista);
            File.WriteAllText(path + "Mekaniker.json", json);


            
    }
        public List<Användare> Returnera(List<Användare> användare)
        {
            List<Användare> returnlist = new List<Användare>();
            for (int i = 0; i < användare.Count - 1; i++)
            {
                returnlist.Add(användare[i + 1]);
                
            }
            return returnlist;
        }

        public void Returnlist(List<Fordon> fordon)
        {
            
            Bil bil = new Bil();
            Buss buss = new Buss();
            Motorcykel motorcykel = new Motorcykel();
            Lastbil lastbil = new Lastbil();
            string bil1, buss1, motorcykel1, lastbil1;
            List<Bil> billista = new List<Bil>();
            List<Buss> busslista = new List<Buss>();
            List<Motorcykel> motorcykellista = new List<Motorcykel>();
            List<Lastbil> lastbilslista = new List<Lastbil>();

            Console.WriteLine(fordon);
            Console.WriteLine();
            foreach (var f in fordon)
            {
                
                if (f.GetType() == bil.GetType())
                {
                    billista.Add((Bil)f);
                }
                if (f.GetType() == buss.GetType())
                {
                    busslista.Add((Buss)f);
                }
                if (f.GetType() == motorcykel.GetType())
                {
                    motorcykellista.Add((Motorcykel)f);
                }
                if (fordon.GetType() == lastbil.GetType())
                {
                    lastbilslista.Add((Lastbil)f);
                }
            }
            bil1 = JsonSerializer.Serialize(billista);
            buss1 = JsonSerializer.Serialize(busslista);
            motorcykel1 = JsonSerializer.Serialize(motorcykellista);
            lastbil1 = JsonSerializer.Serialize(lastbilslista);

           
            File.WriteAllText(path + "Bil.json", bil1);
            File.WriteAllText(path + "Motorcykel.json", motorcykel1);
            File.WriteAllText(path + "Buss.json", buss1);
            File.WriteAllText(path + "Lastbil.json", lastbil1);
        }

        public void Returnärende(List<Ärende> ärende)
        {
            Type type;
            Bromsar bromsar = new Bromsar();
            Vindruta vindruta = new Vindruta();
            Kaross kaross = new Kaross();
            Motor motor = new Motor();
            Däck däck = new Däck();
            string bromsar1, vindruta1, kaross1, motor1, däck1;
            List<Bromsar> bromslista = new List<Bromsar>();
            List<Vindruta> vindrutelista = new List<Vindruta>();
            List<Kaross> karosslista = new List<Kaross>();
            List<Motor> motorlista = new List<Motor>();
            List<Däck> däcklista = new List<Däck>();
            foreach (var f in ärende)
            {
                type = f.GetType();
                if (f.GetType() == bromsar.GetType())
                {
                    bromslista.Add((Bromsar)f);
                }
                if (f.GetType() == vindruta.GetType())
                {
                    vindrutelista.Add((Vindruta)f);
                }
                if (f.GetType() == kaross.GetType())
                {
                    karosslista.Add((Kaross)f);
                }
                if (f.GetType() == motor.GetType())
                {
                    motorlista.Add((Motor)f);
                }
                if (f.GetType() == däck.GetType())
                {
                    däcklista.Add((Däck)f);
                }
            }
            bromsar1 = JsonSerializer.Serialize(bromslista);
            vindruta1 = JsonSerializer.Serialize(vindrutelista);
            kaross1 = JsonSerializer.Serialize(karosslista);
            motor1 = JsonSerializer.Serialize(motorlista);
            däck1 = JsonSerializer.Serialize(däcklista);


            File.WriteAllText(path + "Bromsar.json", bromsar1);
            File.WriteAllText(path + "Vindruta.json", vindruta1);
            File.WriteAllText(path + "kaross.json", kaross1);
            File.WriteAllText(path + "Motor.json", motor1);
            File.WriteAllText(path + "Däck.json", däck1);
        }
    }
}
