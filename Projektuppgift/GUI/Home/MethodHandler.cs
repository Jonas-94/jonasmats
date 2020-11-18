using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Logic.Entities;
using Logic.DAL;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows;
using System.Threading.Tasks;

namespace GUI.Home
{
    public class MethodHandler
    {
        FileLoader fLoader = new FileLoader();
        public HomePage hp { get; set; }


        public async Task SkapaMekaniker()
        {
            //bools för kompetenser
            bool broms = false;
            bool kaross = false;
            bool motor = false;
            bool vindruta = false;
            bool däck = false;
            if (hp.chBoxBroms.IsChecked == true) { broms = true; };
            if (hp.chBoxKaross.IsChecked == true) { kaross = true; };
            if (hp.chBoxMotor.IsChecked == true) { motor = true; };
            if (hp.chBoxVindruta.IsChecked == true) { vindruta = true; };
            if (hp.chBoxDäck.IsChecked == true) { däck = true; };
            int ID = 1;
            List<int> ids = new List<int>();
            ids.Clear();
            for (int i = 0; i < hp.mekSamling.mekaniker.Count; i++)
            {
                ids.Add(hp.mekSamling.mekaniker[i].Id);
            }
            while (ids.Contains(ID))
                ID += 1;
            //Skapar ny mekaniker
            Mekaniker mekaniker = new Mekaniker{Id = ID, Namn = hp.txtBoxName.Text,
                Födelsedatum = hp.txtBoxBirthday.Text,
                Anställningsdatum = hp.txtBoxEmploymentDate.Text,
                Slutdatum = hp.txtBoxUnEmploymentDate.Text,
                Kbromsar = broms,Kkaross = kaross, Kmotor = motor, Kvindruta = vindruta, Kdäck = däck };
            hp.mekSamling.mekaniker.Add(mekaniker);
            
            //Refreshar DataGrid, nollställer och lägger in listan på nytt
            hp.dataGridMekaniker.ItemsSource = null;
            hp.dataGridMekaniker.ItemsSource = hp.mekSamling.mekaniker;
            ResetMechText();
        }
        public void TaBortMekaniker()
        {
            for (int i = 0; i < hp.mekSamling.mekaniker.Count; i++)
                if (hp.dataGridMekaniker.SelectedItem == hp.mekSamling.mekaniker[i])
                {
                    hp.mekSamling.mekaniker.RemoveAt(i);
                    
                }
            hp.dataGridMekaniker.ItemsSource = null;
            hp.dataGridMekaniker.ItemsSource = hp.mekSamling.mekaniker;
        }
        public void ResetMechText()
        {
            hp.chBoxBroms.IsChecked = false; hp.chBoxKaross.IsChecked = false; hp.chBoxMotor.IsChecked = false;
            hp.chBoxVindruta.IsChecked = false; hp.chBoxDäck.IsChecked = false;
            hp.txtBoxName.Text = "";
            hp.txtBoxBirthday.Text = "";
            hp.txtBoxEmploymentDate.Text = "";
            hp.txtBoxUnEmploymentDate.Text = "";
        }
        #region Skapa Fordon
        public void SkapaFordon()
        {
            bool broms = false;
            bool kaross = false;
            bool motor = false;
            bool vindruta = false;
            bool däck = false;
            if (hp.chBoxÄBroms.IsChecked == true) { broms = true; };
            if (hp.chBoxÄKaross.IsChecked == true) { kaross = true; };
            if (hp.chBoxÄMotor.IsChecked == true) { motor = true; };
            if (hp.chBoxÄVindruta.IsChecked == true) { vindruta = true; };
            if (hp.chBoxÄDäck.IsChecked == true) { däck = true; };

            if (hp.fordonTypeMenu.Text == "Bil")
            {
                Bil bil = new Bil
                {
                    Modellnamn = hp.txtTb2modellNamn.Text,
                    Registreringsnummer = hp.txtTb2RegNr.Text,
                    Registreringsdatum = hp.txtTb2RegDate.Text,
                    Äbromsar = broms,
                    Äkaross = kaross,
                    Ämotor = motor,
                    Ävindruta = vindruta,
                    Ädäck = däck
                };
                hp.bilSamling.Bilar.Add(bil);
                
            }
            else if (hp.fordonTypeMenu.Text == "Buss")
            {
                Buss buss = new Buss
                {
                    Modellnamn = hp.txtTb2modellNamn.Text,
                    Registreringsnummer = hp.txtTb2RegNr.Text,
                    Registreringsdatum = hp.txtTb2RegDate.Text,
                    Äbromsar = broms,
                    Äkaross = kaross,
                    Ämotor = motor,
                    Ävindruta = vindruta,
                    Ädäck = däck
                };
                hp.bussSamling.Bussar.Add(buss);
                
            }
            else if (hp.fordonTypeMenu.SelectedItem == "Lastbil")
            {
                Lastbil lastbil = new Lastbil
                {
                    Modellnamn = hp.txtTb2modellNamn.Text,
                    Registreringsnummer = hp.txtTb2RegNr.Text,
                    Registreringsdatum = hp.txtTb2RegDate.Text,
                    Äbromsar = broms,
                    Äkaross = kaross,
                    Ämotor = motor,
                    Ävindruta = vindruta,
                    Ädäck = däck
                };
                hp.lastbilSamling.lastbilar.Add(lastbil);
                
            }
            else if (hp.fordonTypeMenu.SelectedItem == "Motorcykel")
            {
                Motorcykel motorcykel = new Motorcykel
                {
                    Modellnamn = hp.txtTb2modellNamn.Text,
                    Registreringsnummer = hp.txtTb2RegNr.Text,
                    Registreringsdatum = hp.txtTb2RegDate.Text,
                    Äbromsar = broms,
                    Äkaross = kaross,
                    Ämotor = motor,
                    Ävindruta = vindruta,
                    Ädäck = däck
                };
                hp.motorcykelSamling.motorcyklar.Add(motorcykel);
                
            }
            
            
            hp.dataGridFordon.ItemsSource = null;
            hp.dataGridFordon.ItemsSource = hp.fordonSamling.fordon;
            hp.tb3fordonDataGrid.ItemsSource = null;
            hp.tb3fordonDataGrid.ItemsSource = hp.fordonSamling.fordon;
           
        }
        #endregion

        #region Skapa User 
        public void SkapaUser()
        {
            bool good = false;
            try
            {
                User user = new User { Username = hp.txtBoxCreateUsername.Text,
                    Password = hp.txtBoxCreatePassword.Text, ID = hp.tab4ID };
                foreach (var u in hp.userSamling.users)
                {
                    if (!u.Username.Contains(hp.txtBoxCreateUsername.Text)) good = true;
                    if (u.Username.Contains(hp.txtBoxCreateUsername.Text)) good = false; break;
                }
                if (good == true)
                {
                    hp.userSamling.users.Add(user);
                    System.Windows.MessageBox.Show($"Användare {hp.txtBoxCreateUsername.Text} skapades.");
                    hp.txtBoxCreateUsername.Text = "";
                    hp.txtBoxCreatePassword.Text = "";
                    hp.tab4ID = -1;
                    hp.tb4btnChooseUser.Background = Brushes.Gray;
                    CreateUserGrid();
                }
                else
                    System.Windows.MessageBox.Show("Kunde inte skapa kontot.\n Kontrollera att du valt en mekaniker eller välj nytt Username");
            }
            catch (Exception usercreate) { System.Windows.MessageBox.Show("Nånting gick fel. \nKontrollera att du valt en mekaniker och försök igen"); }
        }
        #endregion 

        #region Kompetens och Ärende-jämförelse mellan Fordon o Mekaniker
        public void VisaMekanikerTillFordon()
        {
            List<bool> ärendeBools = new List<bool>();
            ärendeBools.Clear();
            MekanikerSamling ms = new MekanikerSamling();
            hp.tb3mekDataGrid1.ItemsSource = null;
            for (int j = 0; j < hp.fordonSamling.fordon.Count; j++)
            {
                if (hp.tb3fordonDataGrid.SelectedItem == hp.fordonSamling.fordon[j])
                {
                    ärendeBools.Add(hp.fordonSamling.fordon[j].Äbromsar);
                    ärendeBools.Add(hp.fordonSamling.fordon[j].Äkaross);
                    ärendeBools.Add(hp.fordonSamling.fordon[j].Ämotor);
                    ärendeBools.Add(hp.fordonSamling.fordon[j].Ädäck);
                    ärendeBools.Add(hp.fordonSamling.fordon[j].Ävindruta);
                    hp.ärendeFordon = hp.fordonSamling.fordon[j];
                    if (hp.ärendeFordon is Bil)
                        hp.fordonindex = HittaIndex<Bil>(hp.ärendeFordon as Bil, hp.bilSamling.Bilar);
                    if (hp.ärendeFordon is Buss)
                        hp.fordonindex = HittaIndex<Buss>(hp.ärendeFordon as Buss, hp.bussSamling.Bussar);
                    if (hp.ärendeFordon is Lastbil)
                        hp.fordonindex = HittaIndex<Lastbil>(hp.ärendeFordon as Lastbil, hp.lastbilSamling.lastbilar);
                    if (hp.ärendeFordon is Motorcykel)
                        hp.fordonindex = HittaIndex<Motorcykel>(hp.ärendeFordon as Motorcykel, hp.motorcykelSamling.motorcyklar);
                }
            }
            bool run = true;
            while (run)
            {
                for (int i = 0; i < hp.mekSamling.mekaniker.Count; i++)
                {
                    if (ärendeBools[0] == true && hp.mekSamling.mekaniker[i].Kbromsar == false)
                        continue;
                    else if (ärendeBools[1] == true && hp.mekSamling.mekaniker[i].Kkaross == false)
                        continue;
                    else if (ärendeBools[2] == true && hp.mekSamling.mekaniker[i].Kmotor == false)
                        continue;
                    else if (ärendeBools[3] == true && hp.mekSamling.mekaniker[i].Kdäck == false)
                        continue;
                    else if (ärendeBools[4] == true && hp.mekSamling.mekaniker[i].Kvindruta == false)
                        continue;
                    else
                        ms.mekaniker.Add(hp.mekSamling.mekaniker[i]);
                }
                run = false;
            }
            hp.tb3mekDataGrid1.ItemsSource = null;
            hp.tb3mekDataGrid1.ItemsSource = ms.mekaniker;
            if (hp.ärendeFordon != null)
            {
                hp.tb3btnFordon.IsEnabled = false;
                hp.tb3btnMek.Background = Brushes.Green;
            }
            if (hp.tb3btnFordon.Background == Brushes.Green && hp.tb3btnMek.Background == Brushes.Green)
                hp.tb3btnSkapaÄrende.Background = Brushes.Green;
        }
        public void VisaFordonTillMekaniker()
        {
                List<bool> kompetensBools = new List<bool>();
                kompetensBools.Clear();
                FordonSamling fs = new FordonSamling();
                fs.fordon.Clear();
                hp.tb3fordonDataGrid.ItemsSource = null;
                for (int j = 0; j < hp.mekSamling.mekaniker.Count; j++)
                {
                    if (hp.tb3mekDataGrid1.SelectedItem == hp.mekSamling.mekaniker[j])
                    {
                        kompetensBools.Add(hp.mekSamling.mekaniker[j].Kbromsar);
                        kompetensBools.Add(hp.mekSamling.mekaniker[j].Kkaross);
                        kompetensBools.Add(hp.mekSamling.mekaniker[j].Kmotor);
                        kompetensBools.Add(hp.mekSamling.mekaniker[j].Kdäck);
                        kompetensBools.Add(hp.mekSamling.mekaniker[j].Kvindruta);
                        hp.ärendeMekaniker = hp.mekSamling.mekaniker[j];
                        hp.tb3mekLabel.Content = hp.ärendeMekaniker;
                        hp.mekint = HittaIndex<Mekaniker>(hp.ärendeMekaniker, hp.mekSamling.mekaniker);
                    }
                }

                bool run = true;
                while (run)
                {
                    for (int i = 0; i < hp.fordonSamling.fordon.Count; i++)
                    {
                        if (fs.fordon.Contains(hp.fordonSamling.fordon[i]))
                            continue;
                        if (kompetensBools[0] == false && hp.fordonSamling.fordon[i].Äbromsar == true)
                            continue;
                        if (kompetensBools[1] == false && hp.fordonSamling.fordon[i].Äkaross == true)
                            continue;
                        if (kompetensBools[2] == false && hp.fordonSamling.fordon[i].Ämotor == true)
                            continue;
                        if (kompetensBools[3] == false && hp.fordonSamling.fordon[i].Ädäck == true)
                            continue;
                        if (kompetensBools[4] == false && hp.fordonSamling.fordon[i].Ävindruta == true)
                            continue;
                        else
                            fs.fordon.Add(hp.fordonSamling.fordon[i]);
                    }
                    run = false;
                }
                hp.tb3fordonDataGrid.ItemsSource = null;
                hp.tb3fordonDataGrid.ItemsSource = fs.fordon;
            if (hp.ärendeMekaniker != null) 
                    {
                hp.tb3btnMek.Background = Brushes.Green;
                hp.tb3btnMek.IsEnabled = false;
                    }
                if (hp.tb3btnFordon.Background == Brushes.Green && hp.tb3btnMek.Background == Brushes.Green)
                    hp.tb3btnSkapaÄrende.Background = Brushes.Green; 
            }
        #endregion

        #region Skapa Ärende
        public void SkapaÄrende()
        {
            Ärende ärende = new Ärende();
            string beskrivning = ärende.BeskrivningsMetod(hp.ärendeFordon);
            if (hp.ärendeFordon != null && hp.ärendeMekaniker != null)
            {
                hp.tb3btnFordon.Background = Brushes.NavajoWhite;
                hp.tb3btnMek.Background = Brushes.NavajoWhite;
                hp.tb3btnSkapaÄrende.Background = Brushes.NavajoWhite;
                hp.tb3btnFordon.IsEnabled = true;
                hp.tb3btnMek.IsEnabled = true;
                if (hp.mekSamling.mekaniker[hp.mekint].Ärenden < 2)
                {
                    hp.mekSamling.mekaniker[hp.mekint].Ärenden += 1;
                    if (hp.ärendeFordon is Bil)
                    {
                        ärende.SkapaÄrende(beskrivning, hp.bilSamling.Bilar[hp.fordonindex], hp.mekSamling.mekaniker[hp.mekint]);

                        hp.ärendeSamling.ärenden.Add(ärende);
                        
                        
                    }
                    else if (hp.ärendeFordon is Buss)
                    {
                        hp.bussSamling.Bussar[hp.fordonindex].Id = hp.mekSamling.mekaniker[hp.mekint].Id;
                        ärende.SkapaÄrende(beskrivning,
                            hp.bussSamling.Bussar[hp.fordonindex],
                            hp.mekSamling.mekaniker[hp.mekint]);

                        hp.ärendeSamling.ärenden.Add(ärende);
                        
                    }
                    else if (hp.ärendeFordon is Lastbil)
                    {
                        hp.lastbilSamling.lastbilar[hp.fordonindex].Id = hp.mekSamling.mekaniker[hp.mekint].Id;
                        ärende.SkapaÄrende(beskrivning,
                            hp.lastbilSamling.lastbilar[hp.fordonindex],
                            hp.mekSamling.mekaniker[hp.mekint]);
                        hp.ärendeSamling.ärenden.Add(ärende);
                        
                    }
                    else if (hp.ärendeFordon is Motorcykel)
                    {
                        hp.motorcykelSamling.motorcyklar[hp.fordonindex].Id = hp.mekSamling.mekaniker[hp.mekint].Id;
                        ärende.SkapaÄrende(beskrivning,
                            hp.motorcykelSamling.motorcyklar[hp.fordonindex],
                            hp.mekSamling.mekaniker[hp.mekint]);
                        hp.ärendeSamling.ärenden.Add(ärende);
                        
                    }
                    hp.tbÄrendeDataGrid.ItemsSource = null;
                    hp.tbÄrendeDataGrid.ItemsSource = hp.ärendeSamling.ärenden;
                    System.Windows.MessageBox.Show($"Mekanikern {hp.ärendeMekaniker.Namn} " +
                        $"har tagit ärendet gällande fordonet med RegNr: {hp.ärendeFordon.Registreringsnummer}");
                }
                else
                {
                    hp.tb3btnFordon.Background = Brushes.NavajoWhite;
                    hp.tb3btnMek.Background = Brushes.NavajoWhite;
                    hp.tb3btnSkapaÄrende.Background = Brushes.NavajoWhite;
                    hp.tb3btnFordon.IsEnabled = true;
                    hp.tb3btnMek.IsEnabled = true;
                    System.Windows.MessageBox.Show("Mekanikern kan inte ta fler ärenden.");
                }
                
            }
        }
        #endregion
        public void HittaFordonIndex(Object fordon, int fordonindex, List<Bil> billist, List<Buss> busslist, List<Lastbil> lastbil, List<Motorcykel> mclist)
        {
            if (fordon is Bil)
                fordonindex = HittaIndex<Bil>(fordon as Bil, billist);
            else if (fordon is Buss)
                fordonindex = HittaIndex<Buss>(fordon as Buss, busslist);
            else if (fordon is Lastbil)
                fordonindex = HittaIndex<Lastbil>(fordon as Lastbil, lastbil);
            else if (fordon is Motorcykel)
                fordonindex = HittaIndex<Motorcykel>(fordon as Motorcykel, mclist);
        }
        public void CreateUserGrid()
        {
            hp.tb4UserDataGrid.ItemsSource = null;
            hp.meksWithoutUser.Clear();
            List<int> userIDs = new List<int>();
            userIDs.Clear();
            foreach (var user in hp.userSamling.users)
                userIDs.Add(user.ID);
            foreach (var mek in hp.mekSamling.mekaniker)
            {
                if (!userIDs.Contains(mek.Id) && !hp.meksWithoutUser.Contains(mek))
                    hp.meksWithoutUser.Add(mek);
            }
            hp.tb4UserDataGrid.ItemsSource = hp.meksWithoutUser;
        }
        public int HittaIndex<T>(T item, List<T> list)
        {
            return list.IndexOf(item);
        }

        #region Pågående/Lediga ärenden-switch på Skapa Ärende-Fliken
        public void ÄrendePågåendeTillgängligSwitch()
        {
            if (hp.onoffÄrende == false)
            {
                List<Fordon> pågåendeFordon = new List<Fordon>();
                List<Mekaniker> pågåendeMekaniker = new List<Mekaniker>();
                hp.tb3mekDataGrid1.ItemsSource = null;
                hp.tb3mekDataGrid1.ItemsSource = null;
                foreach (var f in hp.fordonSamling.fordon)
                {
                    if (f.Id > 0 && !pågåendeFordon.Contains(f))
                        pågåendeFordon.Add(f);
                }
                hp.tb3fordonDataGrid.ItemsSource = pågåendeFordon;
                foreach (var m in hp.mekSamling.mekaniker)
                {
                    if (m.Ärenden > 0)
                        pågåendeMekaniker.Add(m);
                }
                hp.tb3mekDataGrid1.ItemsSource = pågåendeMekaniker;
                hp.tb3påellerskapaÄrende.Content = "Visa lediga ärenden";
                hp.tb3btnSkapaÄrende.Visibility = Visibility.Hidden;
                hp.onoffÄrende = true;
            }
            else if (hp.onoffÄrende == true)
            {
                VisaLedigaÄrenden();
            }
        }
        public void VisaLedigaÄrenden()
        {
            hp.tb3fordonDataGrid.ItemsSource = null;
            hp.tb3mekDataGrid1.ItemsSource = null;
            List<Fordon> ledigaFordon = new List<Fordon>();
            List<Mekaniker> ledigaMekaniker = new List<Mekaniker>();
            ledigaFordon.Clear();
            ledigaMekaniker.Clear();
            foreach (var f in hp.fordonSamling.fordon)
            {
                if (f.Id == 0 && !ledigaFordon.Contains(f))
                    ledigaFordon.Add(f);
            }
            hp.tb3fordonDataGrid.ItemsSource = ledigaFordon;
            foreach (var m in hp.mekSamling.mekaniker)
            {
                if (m.Ärenden == 0)
                    ledigaMekaniker.Add(m);
            }
            hp.tb3mekDataGrid1.ItemsSource = ledigaMekaniker;
        }
        #endregion
       
    }
}