using System;
using System.Collections.Generic;
using System.Text;
using Logic.DAL;

namespace Logic.Entities
{
    class Admin:Användare
    {
        
        public Verkstad verkstad;

        
        public Fordon Skapafordon(string typ, string modell, string regnr, string drivmedel, decimal milmätare, int däck, string regdatum, bool dragkrok = false, decimal maxvikt = 100, int maxpassagerare = 25)
        {
            if (typ == "Bil")
            {
                Bil bil = new Bil();
                Standardfordon( bil, modell, regnr, drivmedel, milmätare, däck, regdatum);
                bil.Dragkrok = dragkrok;
                return bil;
            
            }
            if (typ == "Lastbil")
            {
                Lastbil lastbil = new Lastbil();
                Standardfordon(lastbil, modell, regnr, drivmedel, milmätare, däck, regdatum);
                lastbil.Maxvikt = maxvikt;
                return lastbil;
            }
            if (typ == "Buss")
            {
                Buss buss = new Buss();
                Standardfordon(buss, modell, regnr, drivmedel, milmätare, däck, regdatum);
                buss.Antalpassagerare = maxpassagerare;
                return buss;
            }
            if (typ == "Motorcykel")
            {
                Motorcykel motorcykel = new Motorcykel();
                Standardfordon(motorcykel, modell, regnr, drivmedel, milmätare, däck, regdatum);
                return motorcykel;
            }
            return new Bil();
        }

        public void Standardfordon(Fordon fordon, string modell, string regnr, string drivmedel, decimal milmätare, int däck, string regdatum)
        {

            fordon.Drivmedel = drivmedel;
            fordon.Däck = däck;
            fordon.Milmätare = milmätare;
            fordon.Modellnamn = modell;
            fordon.Registreringsnummer = regnr;
            fordon.Registreringsdatum = regdatum;
            
            
        }
        public void Läggtillanvändare(string användarnamn, string lösenord, Mekaniker mekaniker)
        {
            if (regex.VerifyEmail(användarnamn) && regex.VerifyPassword (lösenord))
            {
                Användare användare = new Användare();
                användare.Startaden(mekaniker);
                användare.Lösenord = lösenord;
                användare.Användarnamn = användarnamn;

                verkstad.användarelista.Add(användare);
            }

        }
        public void Läggatillmekaniker(string namn, string födelsedatum, string anställningsdatum, string slutdatum)
        {
            Mekaniker mekaniker = new Mekaniker();
            mekaniker.Namn = namn;
            mekaniker.Födelsedatum = födelsedatum;
            mekaniker.Anställningsdatum = anställningsdatum;
            mekaniker.Slutdatum = slutdatum;
            mekaniker.Kbromsar = false;
            mekaniker.Kdäck = false;
            mekaniker.Kkaross = false;
            mekaniker.Kmotor = false;
            mekaniker.Kvindruta = false;
            verkstad.mekanikerlista.Add(mekaniker);
        }
        public void Tabortmekaniker(Mekaniker mekaniker)
        {
            for (int i = 0; i < verkstad.användarelista.Count; i++)
            {
                try
                {
                    if (verkstad.användarelista[i].mekaniker.Id == mekaniker.Id)
                    {

                        verkstad.användarelista.Remove(verkstad.användarelista[i]);
                    }
                }
                catch (Exception )
                {
                    
                   
                }
               
            }
            for (int i = 0; i < verkstad.mekanikerlista.Count; i++)
            {
                if (verkstad.mekanikerlista[i].Id == mekaniker.Id)
                {
                    verkstad.mekanikerlista.Remove(verkstad.mekanikerlista[i]);
                    
                }
            }
        }

        public void Tabortanvändare(Användare användare)
        {
            for (int i = 0; i < verkstad.användarelista.Count; i++)
            {
                try
                {
                    if (verkstad.användarelista[i].Mekanikerid == användare.Mekanikerid)
                    {

                        verkstad.användarelista.Remove(verkstad.användarelista[i]);
                    }
                }
                catch (Exception)
                {
                    

                }

            }
        }


        public void Läggtillkpmpetens(string kompetens, string namn)
        {
            for (int i = 0; i < verkstad.mekanikerlista.Count; i++)
            {
                if (verkstad.mekanikerlista[i].Namn == namn)
                {
                    verkstad.mekanikerlista[i].Ändrakompetens(kompetens);
                    // den mekanikern som vi väljer i listan ändrar vi typ på.
                }
            }
        }
        public void Mekanikeriärende(Ärende ärende, Mekaniker mekaniker)
        {
            if (ärende.Mekaniker != null)
            {
                ärende.Mekaniker.idlista.Remove(ärende.Id);
                ärende.Mekaniker.märendelista.Remove(ärende);
            }

            if ((mekaniker.Getaktivaärenden() < 2) && (Jämförkompetens(ärende, mekaniker)))
            {

                        mekaniker.märendelista.Add(ärende);
                        ärende.Mekaniker = mekaniker;
                        ärende.Pågåendeärende = true;
                
            }


        }

        public bool Jämförkompetens(Ärende ärende, Mekaniker mekaniker)
        {
            bool output = false;
            Bromsar bromsar = new Bromsar();
            Motor motor = new Motor();
            Kaross kaross = new Kaross();
            Vindruta vindruta = new Vindruta();
            Däck däck = new Däck();
            
            if (ärende.GetType() == bromsar.GetType()) 
            {
                output = mekaniker.Kbromsar;

            }
            
            if (ärende.GetType() == motor.GetType())
            {
                output = mekaniker.Kmotor;

            }
            
            if (ärende.GetType() == kaross.GetType())
            {
                output = mekaniker.Kkaross;

            }
            
            if (ärende.GetType() == vindruta.GetType())
            {
                output = mekaniker.Kvindruta;

            }
            
            if (ärende.GetType() == däck.GetType())
            {
                output = mekaniker.Kdäck;

            }
            
            return output;
            

        }
        public void Läggtillärenden(Ärende ärende, Fordon fordon, string beskrivning)
        {

            ärende.fordon = fordon;
            ärende.Beskrivning = beskrivning;
            verkstad.ärendelista.Add(ärende);
            
        }

        public void Tabortärende(Ärende ärende)
        {
            if (ärende.Mekaniker != null)
            {
                ärende.Mekaniker.märendelista.Remove(ärende);
            }
                verkstad.ärendelista.Remove(ärende);
        }
        public void Utförtärende(Ärende ärende)
        {
            ärende.Utförärende();
        }

        public void Redigeraärende(Ärende ärende, Fordon fordon, string typ , string beskrivning)
        {
            ärende.Beskrivning = beskrivning;
            ärende.fordon = fordon;
            if (typ == "Bromsar")
            {
                Bromsar bärende = new Bromsar();


                Mekaniker mekaniker = ärende.Mekaniker;

                Allametoder(ärende, fordon, bärende, mekaniker, beskrivning);

            } 

            else if (typ == "Motor")
            {
                Motor bärende = new Motor();
                
                
                Mekaniker mekaniker = ärende.Mekaniker;
                Allametoder(ärende,fordon,bärende,mekaniker, beskrivning);
                
            }
            else if (typ == "Kaross")
            {
                Kaross bärende = new Kaross();
               
                Mekaniker mekaniker = ärende.Mekaniker;
                Allametoder(ärende, fordon, bärende, mekaniker, beskrivning);
            }
            else if (typ == "Vindruta")
            {
                Vindruta bärende = new Vindruta();
                
                Mekaniker mekaniker = ärende.Mekaniker;
                Allametoder(ärende, fordon, bärende, mekaniker, beskrivning);
            }
            else if (typ == "Däck")
            {
                Däck bärende = new Däck();
                
                Mekaniker mekaniker = ärende.Mekaniker;
                Allametoder(ärende, fordon, bärende, mekaniker, beskrivning);
            }

            ärende.Fid = fordon.Id;

        }

        private void Allametoder(Ärende ärende, Fordon fordon, Ärende bärende, Mekaniker mekaniker, string beskrivning)
        {
            Tabortärende(ärende);
            
            Läggtillärenden(bärende, fordon, beskrivning);
            
            if (mekaniker != null)
            {
                
              Mekanikeriärende(bärende, mekaniker);
            }
        }

       
    }
}
