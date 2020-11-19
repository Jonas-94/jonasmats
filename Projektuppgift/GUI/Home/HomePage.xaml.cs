using Logic.DAL;
using Logic.Entities;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Linq;
namespace GUI.Home
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        FileLoader fLoader { get; set; } = new FileLoader();
        public MekanikerSamling mekSamling { get; set; } = new MekanikerSamling();
        public FordonSamling fordonSamling { get; set; } = new FordonSamling();
        public ÄrendeSamling ärendeSamling { get; set; } = new ÄrendeSamling();
        public MotorcykelSamling motorcykelSamling { get; set; } = new MotorcykelSamling();
        public BilSamling bilSamling { get; set; } = new BilSamling();
        public BussSamling bussSamling { get; set; } = new BussSamling();
        public UserSamling userSamling { get; set; } = new UserSamling();
        public LastbilSamling lastbilSamling { get; set; } = new LastbilSamling();
        public IDSamling idSamling { get; set; } = new IDSamling();
        public KomponentSamling komponentSamling { get; set; } = new KomponentSamling();

        public List<Mekaniker> meksWithoutUser = new List<Mekaniker>();
        public Mekaniker ärendeMekaniker { get; set; }
        public Fordon ärendeFordon { get; set; }
        public int fordonindex { get; set; }
        public int mekint { get; set; }
        public int tab4ID { get; set; }
        public bool onoffÄrende { get; set; }
        string missingFile { get; set; }

        public HomePage()
        {
            InitializeComponent();
            
            onoffÄrende = false;
                System.Windows.MessageBox.Show($"Öppna DAL-mappen som innehåller Json-filerna");
                //FindFolderPath();
                //LoadAllFiles();
                //if(missingFile != null)
                    System.Windows.MessageBox.Show($"Json-filer som saknas kommer skapas automatiskt.\n{missingFile}");
            
            //FordonReload();
            LaddaKomponentTab();
            try
            {
                dataGridFordon.ItemsSource = fordonSamling.fordon;
                tbÄrendeDataGrid.ItemsSource = ärendeSamling.ärenden;
                dataGridMekaniker.ItemsSource = mekSamling.mekaniker;
                tb3fordonDataGrid.ItemsSource = fordonSamling.fordon;
                tb3mekDataGrid1.ItemsSource = mekSamling.mekaniker;
                //CreateUserGrid();
                
            }
            catch { }
        }
        /*
        public void FordonReload()
        {
            fLoader.FordonReload(fordonSamling, bilSamling, bussSamling, lastbilSamling, motorcykelSamling);
        }
        public void LoadAllFiles()
        {
            missingFile = null;
            try
            {
                fLoader.LoadBilar(bilSamling);
            }
            catch (Exception a) { if (a.Source != null) missingFile += "\n"+ a.Message; }
            try
            {
                fLoader.LoadBussar(bussSamling);
            }catch(Exception b) { if (b.Source != null) missingFile += "\n" + b.Message; }
            try
            {
                fLoader.LoadLastbilar();
            }
            catch (Exception c) { if (c.Source != null) missingFile += "\n" + c.Message; }
            try
            {
                fLoader.LoadMotorcyklar(motorcykelSamling);
            }catch(Exception d) { if (d.Source != null) missingFile += "\n" + d.Message; }
            try
            {
                fLoader.LoadÄrenden(ärendeSamling);
            }catch(Exception e) { if (e.Source != null) missingFile += "\n" + e.Message; }
            try
            {
                fLoader.LoadUsers(userSamling);
            }catch(Exception f) { if (f.Source != null) missingFile += "\n" + f.Message; }
            try
            {
                fLoader.LoadMekaniker(mekSamling);
            }
            catch(Exception g) { if (g.Source != null) missingFile += "\n" + g.Message; }
            try
            {
                fLoader.LoadID(idSamling);
            }
            catch (Exception h) { if (h.Source != null) missingFile += "\n" + h.Message; }
            try
            {
                fLoader.LoadKomponenter(komponentSamling);
            }
            catch (Exception i) { if (i.Source != null) missingFile += "\n" + i.Message; }
        }
        */
        //Knappen för att lägga till en ny Mekaniker
        private void BtnCreateMechanic_Click(object sender, RoutedEventArgs e)
        {
            SkapaMekaniker();
            fLoader.SaveMekaniker();
        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        //Knapp som kan ladda DAL-mappen om den inte hittats
        private void laddaMekFil_Click(object sender, RoutedEventArgs e)
        {
            //FindFolderPath();
            try
            {
                //LoadAllFiles();
            }
            catch { System.Windows.MessageBox.Show("Kunde inte ladda mappen."); }
        }
        //metod som tar bort texten i TextBoxar och uncheckar Kompetens-bools i mekanikerfliken

        //metod som spara mekaniker till json
        private void saveMechFile_Click(object sender, RoutedEventArgs e)
        {
            fLoader.SaveMekaniker();
        }
        //Knapp som tar bort vald mekaniker
        private void btnDeleteMechanic_Click(object sender, RoutedEventArgs e)
        {
            TaBortMekaniker();
            fLoader.SaveMekaniker();
        }
        private void CreateUser_Click_1(object sender, RoutedEventArgs e)
        {
            SkapaUser();
            fLoader.SaveUser(userSamling);
        }
        private void BtnCreateFordon_Click_2(object sender, RoutedEventArgs e)
        {
            SkapaFordon();
        }
        private void tb3btnMek_Click(object sender, RoutedEventArgs e)
        {
            //VisaFordonTillMekaniker();
        }
        MekanikerSamling ms { get; set; }
        private void tb3btnFordon_Click(object sender, RoutedEventArgs e)
        {
            //VisaMekanikerTillFordon();
        }
        private void tb3btnSkapaÄrende_Click(object sender, RoutedEventArgs e)
        {
            //SkapaÄrende();
            fLoader.SaveÄrenden();
        }

        private void tb4btnChooseUser_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < mekSamling.mekaniker.Count; i++)
            {
                if (tb4UserDataGrid.SelectedItem == mekSamling.mekaniker[i])
                {
                    tb4btnChooseUser.Background = Brushes.Green;
                    tab4ID = mekSamling.mekaniker[i].Id;
                }
            }
        }
        private void tb3cboxFordon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tb3cboxFordon.SelectedItem == "Alla fordon")
            {
                dataGridFordon.ItemsSource = null;
                dataGridFordon.ItemsSource = fordonSamling.fordon;
            }
            else if (tb3cboxFordon.SelectedItem == "Bilar")
            {
                dataGridFordon.ItemsSource = null;
                dataGridFordon.ItemsSource = bilSamling.Bilar;
            }
            else if (tb3cboxFordon.SelectedItem == "Bussar")
            {
                dataGridFordon.ItemsSource = null;
                dataGridFordon.ItemsSource = bussSamling.Bussar;
            }
            else if (tb3cboxFordon.SelectedItem=="Lastbilar")
            {
                dataGridFordon.ItemsSource = null;
                dataGridFordon.ItemsSource = lastbilSamling.lastbilar;
            }
            else if(tb3cboxFordon.SelectedItem == "Motorcyklar")
            {
                dataGridFordon.ItemsSource = null;
                dataGridFordon.ItemsSource = motorcykelSamling.motorcyklar;
            }
        }
        private void tb3visapåellerskapaÄrende_Click(object sender, RoutedEventArgs e)
        {
            //ÄrendePågåendeTillgängligSwitch();
        }
        /*
        private void btnAvslutaÄrende_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
            for (int i = 0; i < ärendeSamling.ärenden.Count; i++)
            { 
                if (tbÄrendeDataGrid.SelectedItem == ärendeSamling.ärenden[i])
                {
                    ärendeSamling.ärenden[i].ÄrendeStatus = true;
                    foreach (var m in mekSamling.mekaniker)
                        if (m.Id == ärendeSamling.ärenden[i].ÄrendeID)
                        {
                            m.Ärenden -= 1;
                            break;
                        }
                    HittaFordonIndex(fLoader.ärendeSamling.ärenden[i].fordon, index, bilSamling.Bilar,
                        bussSamling.Bussar, lastbilSamling.lastbilar, motorcykelSamling.motorcyklar);
                    if (ärendeSamling.ärenden[i].fordon is Bil)
                        bilSamling.Bilar.RemoveAt(index);
                    else if (ärendeSamling.ärenden[i].fordon is Buss)
                        bussSamling.Bussar.RemoveAt(index);
                    else if (ärendeSamling.ärenden[i].fordon is Lastbil)
                        lastbilSamling.lastbilar.RemoveAt(index);
                    else if (ärendeSamling.ärenden[i].fordon is Motorcykel)
                        motorcykelSamling.motorcyklar.RemoveAt(index);

                    fLoader.SaveAllFordon(bilSamling,bussSamling,lastbilSamling,motorcykelSamling);
                    fLoader.FordonReload(fordonSamling,bilSamling, bussSamling, lastbilSamling, motorcykelSamling);

                    
                    fLoader.SaveMekaniker(mekSamling);
                    tbÄrendeDataGrid.ItemsSource = null;
                    tbÄrendeDataGrid.ItemsSource = ärendeSamling.ärenden;
                } 
            }
        }
        */
        public void FindFolderPath()
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.ShowDialog();
            //fLoader.folderPath = folder.SelectedPath;
        }
        public string FirstLetterCapital(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            char[] letters = str.ToCharArray();
            letters[0] = char.ToUpper(letters[0]);
            return new string(letters);
        }
        public void SkapaFordon()
        {
            bool broms = false;
            bool kaross = false;
            bool motor = false;
            bool vindruta = false;
            bool däck = false;
            bool dragkrok = false;
            if (chBoxÄBroms.IsChecked == true) { broms = true; };
            if (chBoxÄKaross.IsChecked == true) { kaross = true; };
            if (chBoxÄMotor.IsChecked == true) { motor = true; };
            if (chBoxÄVindruta.IsChecked == true) { vindruta = true; };
            if (chBoxÄDäck.IsChecked == true) { däck = true; };
            if (tb2DragKrok.IsChecked == true){ dragkrok = true; };
            
            string drivmedel = "";
            if (tb2Drivmedel.SelectedItem == Bensin)
                drivmedel = "Bensin";
            else if (tb2Drivmedel.SelectedItem == Etanol)
                drivmedel = "Etanol";
            else if (tb2Drivmedel.SelectedItem == El)
                drivmedel = "El";
            else if (tb2Drivmedel.SelectedItem == Diesel)
                drivmedel = "Diesel";

            bool run = true;
            while (run)
            {
                if(broms == false && kaross == false && motor == false && vindruta == false && däck == false)
                {
                    System.Windows.Forms.MessageBox.Show("Välj ett eller flera ärenden.");
                }
                int maxviktOrPassagerare;
                int milmätare = 0;
                string modellNamn = FirstLetterCapital(txtTb2modellNamn.Text);
                string regNr = txtTb2RegNr.Text.ToUpper();
                DateTime datum;
                string regDatum;
                if (DateTime.TryParse(txtTb2RegDate.Text, out datum))
                    regDatum = datum.ToShortDateString();
                else
                {
                    System.Windows.Forms.MessageBox.Show("Fyll i ett giltigt Registreringsdatum (YYYY-MM-DD)");
                    break;
                }
                if (string.IsNullOrEmpty(modellNamn))
                    System.Windows.Forms.MessageBox.Show("Fyll i ett Modellnamn."); run = false;
                if (string.IsNullOrEmpty(regNr))
                {
                    System.Windows.Forms.MessageBox.Show("Fyll i ett RegistreringsNummer.");
                    break;
                }
                foreach (var f in fordonSamling.fordon)
                {
                    if (f.Registreringsnummer == regNr)
                        System.Windows.Forms.MessageBox.Show("Registreringsnumret finns redan i registret.");
                    break;
                }
                if (int.TryParse(txtTb2MilMätare.Text, out int mätare))
                    milmätare = mätare;
                else
                    System.Windows.Forms.MessageBox.Show("Fyll i milmätare (i siffror)");

                if (fordonTypeMenu.SelectedItem == Bil)
                {
                    string _biltyp = "";
                    if (tb2cBoxBilTyp.SelectedItem == Sedan)
                        _biltyp = "Sedan";
                    else if (tb2cBoxBilTyp.SelectedItem == Herrgårdsvagn)
                        _biltyp = "Herrgårdsvagn";
                    else if (tb2cBoxBilTyp.SelectedItem == Cabriolet)
                        _biltyp = "Cabriolet";
                    else if (tb2cBoxBilTyp.SelectedItem == Halvkombi)
                        _biltyp = "Halvkombi";
                    Bil bil = new Bil
                    {
                            Milmätare = milmätare,
                            Drivmedel = drivmedel,
                            Dragkrok = dragkrok,
                            bilTyp = _biltyp,
                            Modellnamn = modellNamn,
                            Registreringsnummer = regNr,
                            Registreringsdatum = regDatum,
                            Äbromsar = broms,
                            Äkaross = kaross,
                            Ämotor = motor,
                            Ävindruta = vindruta,
                            Ädäck = däck
                        };
                    bilSamling.Bilar.Add(bil);
                    fordonSamling.fordon.Add(bil);
                    fLoader.SaveBilar();
                }
                else if (fordonTypeMenu.SelectedItem == Buss)
                {
                    if (!string.IsNullOrEmpty(txtboxMaxviktOrPassagerare.Text) && Int32.TryParse(txtboxMaxviktOrPassagerare.Text, out int siffra))
                        maxviktOrPassagerare = siffra;
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Fyll i giltigt antal passagerare. (i siffror)");
                        break;
                    }
                    Buss buss = new Buss
                    {
                        Milmätare = milmätare,
                        Drivmedel = drivmedel,
                        Antalpassagerare = maxviktOrPassagerare,
                        Modellnamn = modellNamn,
                        Registreringsnummer = regNr,
                        Registreringsdatum = regDatum,
                        Äbromsar = broms,
                        Äkaross = kaross,
                        Ämotor = motor,
                        Ävindruta = vindruta,
                        Ädäck = däck
                    };
                    bussSamling.Bussar.Add(buss);
                    fordonSamling.fordon.Add(buss);
                    fLoader.SaveBussar();

                }
                else if (fordonTypeMenu.SelectedItem == Lastbil)
                {
                    if (!string.IsNullOrEmpty(txtboxMaxviktOrPassagerare.Text)&& Int32.TryParse(txtboxMaxviktOrPassagerare.Text, out int siffra))
                        maxviktOrPassagerare = siffra;
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Fyll i en giltig maxvikt. (i siffror)");
                        break;
                    }
                    Lastbil lastbil = new Lastbil
                    {
                        Milmätare = milmätare,
                        Drivmedel = drivmedel,
                        Maxvikt = maxviktOrPassagerare,
                        Modellnamn = modellNamn,
                        Registreringsnummer = regNr,
                        Registreringsdatum = regDatum,
                        Äbromsar = broms,
                        Äkaross = kaross,
                        Ämotor = motor,
                        Ävindruta = vindruta,
                        Ädäck = däck
                    };
                    lastbilSamling.lastbilar.Add(lastbil);
                    fLoader.SaveLastBilar();
                }
                else if (fordonTypeMenu.SelectedItem == Motorcykel)
                {
                    Motorcykel motorcykel = new Motorcykel
                    {
                        Milmätare = milmätare,
                        Drivmedel = drivmedel,
                        Modellnamn = modellNamn,
                        Registreringsnummer = regNr,
                        Registreringsdatum = regDatum,
                        Äbromsar = broms,
                        Äkaross = kaross,
                        Ämotor = motor,
                        Ävindruta = vindruta,
                        Ädäck = däck
                    };
                    motorcykelSamling.motorcyklar.Add(motorcykel);
                    fLoader.SaveMotorcyklar();
                }
                //FordonReload();
                dataGridFordon.ItemsSource = null;
                dataGridFordon.ItemsSource = fordonSamling.fordon;
                tb3fordonDataGrid.ItemsSource = null;
                tb3fordonDataGrid.ItemsSource = fordonSamling.fordon;
                run = false;
            }
        }
        public void SkapaMekaniker()
        {
            //bools för kompetenser
            bool broms = false;
            bool kaross = false;
            bool motor = false;
            bool vindruta = false;
            bool däck = false;
            if (chBoxBroms.IsChecked == true) { broms = true; };
            if (chBoxKaross.IsChecked == true) { kaross = true; };
            if (chBoxMotor.IsChecked == true) { motor = true; };
            if (chBoxVindruta.IsChecked == true) { vindruta = true; };
            if (chBoxDäck.IsChecked == true) { däck = true; };
            int nyID = 1;
            try
            {
                List<int> idlist = new List<int>();
                foreach (var i in idSamling.idlista)
                    idlist.Add(i.IDnr);
                nyID = idlist.Max();
            }
            catch { }

            bool run = true;
            while (run)
            {
                string _namn = FirstLetterCapital(txtBoxName.Text);
                string _födelsedatum = "";
                string _anställningsdatum = "";
                string _slutdatum = "";
                if (string.IsNullOrEmpty(txtBoxName.Text))
                {
                    System.Windows.Forms.MessageBox.Show("Fyll i ett namn.");
                }
                if (DateTime.TryParse(txtBoxBirthday.Text, out DateTime bDay))
                {
                    _födelsedatum = bDay.ToShortDateString();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Fyll i ett giltigt födelsedatum (YYYY-MM-DD)");
                    break;
                }
                if (DateTime.TryParse(txtBoxEmploymentDate.Text, out DateTime aDay))
                    _anställningsdatum = aDay.ToShortDateString();
                else
                {
                    System.Windows.Forms.MessageBox.Show("Fyll i ett giltigt anställningsdatum (YYYY-MM-DD)");
                    break;
                }
                if (DateTime.TryParse(txtBoxUnEmploymentDate.Text, out DateTime uDay))
                {
                    _slutdatum = uDay.ToShortDateString();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Fyll i ett giltigt slutdatum (YYYY-MM-DD)");
                    break;
                }
                while (nyID == 0 || mekSamling.mekaniker.Exists(m => m.Id.Equals(nyID)))
                {
                        nyID += 1; 
                }
                //Skapar ny mekaniker
                Mekaniker mekaniker = new Mekaniker
                {
                    Id = nyID,
                    förnamn = _namn,
                    Födelsedatum = _födelsedatum,
                    Anställningsdatum = _anställningsdatum,
                    Slutdatum = _slutdatum,
                    Kbromsar = broms,
                    Kkaross = kaross,
                    Kmotor = motor,
                    Kvindruta = vindruta,
                    Kdäck = däck
                };
                ID id = new ID { IDnr = nyID };
                idSamling.idlista.Add(id);
                mekSamling.mekaniker.Add(mekaniker);
                //Refreshar DataGrid, nollställer och lägger in listan på nytt
                dataGridMekaniker.ItemsSource = null;
                dataGridMekaniker.ItemsSource = mekSamling.mekaniker;
                fLoader.SaveMekaniker();
                fLoader.SaveID(idSamling);
                ResetMechText();
                break;
            }
        }
        public void TaBortMekaniker()
        {
            for (int i = 0; i < mekSamling.mekaniker.Count; i++)
                if (dataGridMekaniker.SelectedItem == mekSamling.mekaniker[i])
                {
                    mekSamling.mekaniker.RemoveAt(i);
                }
            dataGridMekaniker.ItemsSource = null;
            dataGridMekaniker.ItemsSource = mekSamling.mekaniker;
        }
        public void ResetMechText()
        {
            chBoxBroms.IsChecked = false; chBoxKaross.IsChecked = false; chBoxMotor.IsChecked = false;
            chBoxVindruta.IsChecked = false; chBoxDäck.IsChecked = false;
            txtBoxName.Text = "";
            txtBoxBirthday.Text = "";
            txtBoxEmploymentDate.Text = "";
            txtBoxUnEmploymentDate.Text = "";
        }
        #region Skapa User 
       
        public void SkapaUser()
        {
            bool good = false;
            try
            {
                bool run = true;
                while (run)
                {
                    string username = txtBoxCreateUsername.Text + "@meckarna.com";
                    if (userSamling.users.Exists(user => user.Username.Equals(username)) || string.IsNullOrEmpty(txtBoxCreateUsername.Text))
                    {
                        System.Windows.MessageBox.Show("Välj ett nytt användarnamn");
                        break;
                    }
                    else if (tab4ID <= 0)
                    {
                        System.Windows.MessageBox.Show("Välj en mekaniker.");
                        break;
                    }
                    else if (string.IsNullOrEmpty(txtBoxCreatePassword.Text))
                    {
                        System.Windows.MessageBox.Show("Välj ett lösenord.");
                        break;
                    }

                    User user = new User
                    {
                        Username = username,
                        Password = txtBoxCreatePassword.Text,
                        ID = tab4ID
                    };

                    if (!userSamling.users.Exists(user => user.Username.Equals(username)))
                    {
                        userSamling.users.Add(user);
                        System.Windows.MessageBox.Show($"Användare {username} skapades.");
                        txtBoxCreateUsername.Text = "";
                        txtBoxCreatePassword.Text = "";
                        tab4ID = -1;
                        tb4btnChooseUser.Background = Brushes.Gray;
                        //CreateUserGrid();
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Kunde inte skapa kontot.\n Kontrollera att du valt en mekaniker eller välj nytt Username");
                        break;
                    }
                }
            }
            catch (Exception usercreate) { System.Windows.MessageBox.Show("Nånting gick fel. \nKontrollera att du valt en mekaniker och försök igen"); }
        }
        #endregion 

        #region Kompetens och Ärende-jämförelse mellan Fordon o Mekaniker
     /*
        public void VisaMekanikerTillFordon()
        {
            List<bool> ärendeBools = new List<bool>();
            ärendeBools.Clear();
            MekanikerSamling ms = new MekanikerSamling();
            tb3mekDataGrid1.ItemsSource = null;
            for (int j = 0; j < fordonSamling.fordon.Count; j++)
            {
                if (tb3fordonDataGrid.SelectedItem == fordonSamling.fordon[j])
                {
                    ärendeBools.Add(fordonSamling.fordon[j].Äbromsar);
                    ärendeBools.Add(fordonSamling.fordon[j].Äkaross);
                    ärendeBools.Add(fordonSamling.fordon[j].Ämotor);
                    ärendeBools.Add(fordonSamling.fordon[j].Ädäck);
                    ärendeBools.Add(fordonSamling.fordon[j].Ävindruta);
                    ärendeFordon = fordonSamling.fordon[j];
                    if (ärendeFordon is Bil)
                        fordonindex = HittaIndex<Bil>(ärendeFordon as Bil, bilSamling.Bilar);
                    if (ärendeFordon is Buss)
                        fordonindex = HittaIndex<Buss>(ärendeFordon as Buss, bussSamling.Bussar);
                    if (ärendeFordon is Lastbil)
                        fordonindex = HittaIndex<Lastbil>(ärendeFordon as Lastbil, lastbilSamling.lastbilar);
                    if (ärendeFordon is Motorcykel)
                        fordonindex = HittaIndex<Motorcykel>(ärendeFordon as Motorcykel, motorcykelSamling.motorcyklar);
                }
            }
            bool run = true;
            while (run)
            {
                for (int i = 0; i < mekSamling.mekaniker.Count; i++)
                {
                    if (ärendeBools[0] == true && mekSamling.mekaniker[i].Kbromsar == false)
                        continue;
                    else if (ärendeBools[1] == true && mekSamling.mekaniker[i].Kkaross == false)
                        continue;
                    else if (ärendeBools[2] == true && mekSamling.mekaniker[i].Kmotor == false)
                        continue;
                    else if (ärendeBools[3] == true && mekSamling.mekaniker[i].Kdäck == false)
                        continue;
                    else if (ärendeBools[4] == true && mekSamling.mekaniker[i].Kvindruta == false)
                        continue;
                    else
                        ms.mekaniker.Add(mekSamling.mekaniker[i]);
                }
                run = false;
            }
            tb3mekDataGrid1.ItemsSource = null;
            tb3mekDataGrid1.ItemsSource = ms.mekaniker;
            if (ärendeFordon != null)
            {
                tb3btnFordon.IsEnabled = false;
                tb3btnMek.Background = Brushes.Green;
            }
            if (tb3btnFordon.Background == Brushes.Green && tb3btnMek.Background == Brushes.Green)
                tb3btnSkapaÄrende.Background = Brushes.Green;
        }
        public void VisaFordonTillMekaniker()
        {
            List<bool> kompetensBools = new List<bool>();
            kompetensBools.Clear();
            FordonSamling fs = new FordonSamling();
            fs.fordon.Clear();
            tb3fordonDataGrid.ItemsSource = null;
            for (int j = 0; j < mekSamling.mekaniker.Count; j++)
            {
                if (tb3mekDataGrid1.SelectedItem == mekSamling.mekaniker[j])
                {
                    kompetensBools.Add(mekSamling.mekaniker[j].Kbromsar);
                    kompetensBools.Add(mekSamling.mekaniker[j].Kkaross);
                    kompetensBools.Add(mekSamling.mekaniker[j].Kmotor);
                    kompetensBools.Add(mekSamling.mekaniker[j].Kdäck);
                    kompetensBools.Add(mekSamling.mekaniker[j].Kvindruta);
                    ärendeMekaniker = mekSamling.mekaniker[j];
                    tb3mekLabel.Content = ärendeMekaniker;
                    mekint = HittaIndex<Mekaniker>(ärendeMekaniker, mekSamling.mekaniker);
                }
            }

            bool run = true;
            while (run)
            {
                for (int i = 0; i < fordonSamling.fordon.Count; i++)
                {
                    if (fs.fordon.Contains(fordonSamling.fordon[i]))
                        continue;
                    if (kompetensBools[0] == false && fordonSamling.fordon[i].Äbromsar == true)
                        continue;
                    if (kompetensBools[1] == false && fordonSamling.fordon[i].Äkaross == true)
                        continue;
                    if (kompetensBools[2] == false && fordonSamling.fordon[i].Ämotor == true)
                        continue;
                    if (kompetensBools[3] == false && fordonSamling.fordon[i].Ädäck == true)
                        continue;
                    if (kompetensBools[4] == false && fordonSamling.fordon[i].Ävindruta == true)
                        continue;
                    else
                        fs.fordon.Add(fordonSamling.fordon[i]);
                }
                run = false;
            }
            tb3fordonDataGrid.ItemsSource = null;
            tb3fordonDataGrid.ItemsSource = fs.fordon;
            if (ärendeMekaniker != null)
            {
                tb3btnMek.IsEnabled = false;
            }
            if (tb3btnFordon.IsEnabled == false && tb3btnMek.IsEnabled == false)
                tb3btnSkapaÄrende.Background = Brushes.Green;
        }
        #endregion

        #region Skapa Ärende
        public void SkapaÄrende()
        {
            Ärende ärende = new Ärende();
            string beskrivning = ärende.BeskrivningsMetod(ärendeFordon);
            if (ärendeFordon != null && ärendeMekaniker != null)
            {
                tb3btnFordon.Background = Brushes.NavajoWhite;
                tb3btnMek.Background = Brushes.NavajoWhite;
                tb3btnSkapaÄrende.Background = Brushes.NavajoWhite;
                tb3btnFordon.IsEnabled = true;
                tb3btnMek.IsEnabled = true;
                int ärendeId = mekSamling.mekaniker[mekint].Id;
                if (mekSamling.mekaniker[mekint].Ärenden < 2)
                {
                    mekSamling.mekaniker[mekint].Ärenden += 1;
                    if (ärendeFordon is Bil)
                    {
                        ärende.SkapaÄrende(ärendeId, beskrivning, bilSamling.Bilar[fordonindex], mekSamling.mekaniker[mekint]);
                        ärendeSamling.ärenden.Add(ärende);
                    }
                    else if (ärendeFordon is Buss)
                    {
                        bussSamling.Bussar[fordonindex].Id = mekSamling.mekaniker[mekint].Id;
                        ärende.SkapaÄrende(ärendeId, beskrivning,
                            bussSamling.Bussar[fordonindex],
                            mekSamling.mekaniker[mekint]);
                        ärendeSamling.ärenden.Add(ärende);
                    }
                    else if (ärendeFordon is Lastbil)
                    {
                        lastbilSamling.lastbilar[fordonindex].Id = mekSamling.mekaniker[mekint].Id;
                        ärende.SkapaÄrende(ärendeId,beskrivning,
                            lastbilSamling.lastbilar[fordonindex],
                            mekSamling.mekaniker[mekint]);
                        ärendeSamling.ärenden.Add(ärende);
                    }
                    else if (ärendeFordon is Motorcykel)
                    {
                        motorcykelSamling.motorcyklar[fordonindex].Id = mekSamling.mekaniker[mekint].Id;
                        ärende.SkapaÄrende(ärendeId,beskrivning,
                        motorcykelSamling.motorcyklar[fordonindex],
                        mekSamling.mekaniker[mekint]);
                        ärendeSamling.ärenden.Add(ärende);
                    }
                    tbÄrendeDataGrid.ItemsSource = null;
                    tbÄrendeDataGrid.ItemsSource = ärendeSamling.ärenden;
                    System.Windows.MessageBox.Show($"Mekanikern {ärendeMekaniker.förnamn} " +
                        $"har tagit ärendet gällande fordonet med RegNr: {ärendeFordon.Registreringsnummer}");
                }
                else
                {
                    tb3btnFordon.Background = Brushes.NavajoWhite;
                    tb3btnMek.Background = Brushes.NavajoWhite;
                    tb3btnSkapaÄrende.Background = Brushes.NavajoWhite;
                    tb3btnFordon.IsEnabled = true;
                    tb3btnMek.IsEnabled = true;
                    System.Windows.MessageBox.Show("Mekanikern kan inte ta fler ärenden.");
                }
            }
        }
        #endregion
        public void HittaFordonIndex(Fordon fordon, int fordonindex, List<Bil> billist, List<Buss> busslist, List<Lastbil> lastbil, List<Motorcykel> mclist)
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
            tb4UserDataGrid.ItemsSource = null;
            meksWithoutUser.Clear();
            List<int> userIDs = new List<int>();
            userIDs.Clear();
            foreach (var user in userSamling.users)
                userIDs.Add(user.ID);
            foreach (var mek in mekSamling.mekaniker)
            {
                if (!userIDs.Contains(mek.Id) && !meksWithoutUser.Contains(mek))
                    meksWithoutUser.Add(mek);
            }
            tb4UserDataGrid.ItemsSource = meksWithoutUser;
        }
        public int HittaIndex<T>(T item, List<T> list)
        {
            return list.IndexOf(item);
        }

        #region Pågående/Lediga ärenden-switch på Skapa Ärende-Fliken
        public void ÄrendePågåendeTillgängligSwitch()
        {
            if (onoffÄrende == false)
            {
                List<Fordon> pågåendeFordon = new List<Fordon>();
                List<Mekaniker> pågåendeMekaniker = new List<Mekaniker>();
                tb3mekDataGrid1.ItemsSource = null;
                tb3mekDataGrid1.ItemsSource = null;
                foreach (var f in fordonSamling.fordon)
                {
                    if (f.Id > 0 && !pågåendeFordon.Contains(f))
                        pågåendeFordon.Add(f);
                }
                tb3fordonDataGrid.ItemsSource = pågåendeFordon;
                foreach (var m in mekSamling.mekaniker)
                {
                    if (m.Ärenden > 0)
                        pågåendeMekaniker.Add(m);
                }
                tb3mekDataGrid1.ItemsSource = pågåendeMekaniker;
                tb3påellerskapaÄrende.Content = "Visa lediga ärenden";
                tb3btnSkapaÄrende.Visibility = Visibility.Hidden;
                onoffÄrende = true;
            }
            else if (onoffÄrende == true)
            {
                VisaLedigaÄrenden();
            }
        }
        public void VisaLedigaÄrenden()
        {
            tb3fordonDataGrid.ItemsSource = null;
            tb3mekDataGrid1.ItemsSource = null;
            List<Fordon> ledigaFordon = new List<Fordon>();
            List<Mekaniker> ledigaMekaniker = new List<Mekaniker>();
            ledigaFordon.Clear();
            ledigaMekaniker.Clear();
            foreach (var f in fordonSamling.fordon)
            {
                if (f.Id == 0 && !ledigaFordon.Contains(f))
                    ledigaFordon.Add(f);
            }
            tb3fordonDataGrid.ItemsSource = ledigaFordon;
            foreach (var m in mekSamling.mekaniker)
            {
                if (m.Ärenden == 0)
                    ledigaMekaniker.Add(m);
            }
            tb3mekDataGrid1.ItemsSource = ledigaMekaniker;
        }
        #endregion

        private void fordonTypeMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            fordonChange();
            
        }

        private async Task fordonChange()
        {
            await Task.CompletedTask;
            await Task.Delay(100);
            if (fordonTypeMenu.SelectedItem == Bil)
            {
                KomponentCheck(4, 4);
            }
            if (fordonTypeMenu.SelectedItem == Lastbil || fordonTypeMenu.SelectedItem == Buss)
            {
                KomponentCheck(6, 6);
            }
            if (fordonTypeMenu.SelectedItem == Motorcykel)
            {
                KomponentCheck(2, 2);
            }
            
                
                if (fordonTypeMenu.SelectedItem != Bil)
                        tb2DragKrok.Visibility = Visibility.Hidden;
                    else if (fordonTypeMenu.SelectedItem == Bil)
                        tb2DragKrok.Visibility = Visibility.Visible;
                    if (fordonTypeMenu.SelectedItem != Buss && fordonTypeMenu.SelectedItem != Lastbil)
                    {
                        lblMaxviktOrPassagerare.Visibility = Visibility.Hidden;
                        txtboxMaxviktOrPassagerare.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        lblMaxviktOrPassagerare.Visibility = Visibility.Visible;
                        txtboxMaxviktOrPassagerare.Visibility = Visibility.Visible;
                    }

                    if (fordonTypeMenu.SelectedItem == Buss)
                        lblMaxviktOrPassagerare.Content = "Antal passagerare:";

                    if (fordonTypeMenu.SelectedItem == Lastbil)
                        lblMaxviktOrPassagerare.Content = "Maxvikt:";
                
            
        }
     */
        private void addkomponentbtn_Click(object sender, RoutedEventArgs e)
        {
            Komponenter komponent = new Komponenter();
            if (komponentSamling.komponentlista.Count < 1)
                komponentSamling.komponentlista.Add(komponent);
            bildäcklbl.Content = $"Bildäck: {komponentSamling.komponentlista[0].Bildäck}";
            lastbilsdäcklbl.Content = $"Lastbilsdäck: {komponentSamling.komponentlista[0].Lastbilsdäck}";
            Motorerlbl.Content = $"Motorer: {komponentSamling.komponentlista[0].Motorer}";
            Vindrutorlbl.Content = $"Vindrutor: {komponentSamling.komponentlista[0].Vindrutor}";
            Karosserlbl.Content = $"Karosser: {komponentSamling.komponentlista[0].Karosser}";
            Bromsarlbl.Content = $"Bromsar: {komponentSamling.komponentlista[0].Bromsar}";
            if (
                int.TryParse(bilDäcktxt.Text, out int bdäck) &&
                int.TryParse(Lastbildäcktxt.Text, out  int lbdäck)  &&
                int.TryParse(motorertxt.Text, out int motorer)  &&
                int.TryParse(vindrutatxt.Text, out int vindrutor)  &&
                int.TryParse(karosstxt.Text, out int karosser) &&
                int.TryParse(bromstxt.Text, out int bromsar) 
                )
            {
                komponentSamling.komponentlista[0].Bildäck += bdäck;
                komponentSamling.komponentlista[0].Lastbilsdäck += lbdäck;
                komponentSamling.komponentlista[0].Motorer += motorer;
                komponentSamling.komponentlista[0].Vindrutor += vindrutor;
                komponentSamling.komponentlista[0].Karosser += karosser;
                komponentSamling.komponentlista[0].Bromsar += bromsar;
            }
            else
                System.Windows.Forms.MessageBox.Show("Endast siffror tillåtna");

            bildäcklbl.Content = $"Bildäck: {komponentSamling.komponentlista[0].Bildäck}";
            lastbilsdäcklbl.Content = $"Lastbilsdäck: {komponentSamling.komponentlista[0].Lastbilsdäck}";
            Motorerlbl.Content = $"Motorer: {komponentSamling.komponentlista[0].Motorer}";
            Vindrutorlbl.Content = $"Vindrutor: {komponentSamling.komponentlista[0].Vindrutor}";
            Karosserlbl.Content = $"Karosser: {komponentSamling.komponentlista[0].Karosser}";
            Bromsarlbl.Content = $"Bromsar: {komponentSamling.komponentlista[0].Bromsar}";
            bilDäcktxt.Text = "0";
            Lastbildäcktxt.Text = "0";
            motorertxt.Text = "0";
            vindrutatxt.Text = "0";
            karosstxt.Text = "0";
            bromstxt.Text = "0";
            fLoader.SaveKomponenter(komponentSamling);
        }
        public void LaddaKomponentTab()
        {
            Komponenter komponent = new Komponenter { Lastbilsdäck = 0, Bildäck = 0, Motorer = 0,Vindrutor=0,Karosser=0,Bromsar=0 };
            if (komponentSamling.komponentlista.Count < 1)
                komponentSamling.komponentlista.Add(komponent);
            bildäcklbl.Content = $"Bildäck: {komponentSamling.komponentlista[0].Bildäck}";
            lastbilsdäcklbl.Content = $"Lastbilsdäck: {komponentSamling.komponentlista[0].Lastbilsdäck}";
            Motorerlbl.Content = $"Motorer: {komponentSamling.komponentlista[0].Motorer}";
            Vindrutorlbl.Content = $"Vindrutor: {komponentSamling.komponentlista[0].Vindrutor}";
            Karosserlbl.Content = $"Karosser: {komponentSamling.komponentlista[0].Karosser}";
            Bromsarlbl.Content = $"Bromsar: {komponentSamling.komponentlista[0].Bromsar}";
        }
        private void KomponentCheck(int bromsint, int däckint)
        {
            if (komponentSamling.komponentlista[0].Bromsar < bromsint)
            {
                chBoxÄBroms.IsEnabled = false;
                chBoxÄBroms.Content = "Komponent: bromsar saknas";
            }
            else
            {
                chBoxÄBroms.IsEnabled = true;
                chBoxÄBroms.Content = "Bromsar";
            }

            if (komponentSamling.komponentlista[0].Bildäck < däckint)
            {
                chBoxÄDäck.IsEnabled = false;
                chBoxÄDäck.Content = "Komponent: däck saknas";
            }
            else
            {
                chBoxÄDäck.IsEnabled = true;
                chBoxÄDäck.Content = "Däck";
            }

            if (komponentSamling.komponentlista[0].Vindrutor < 1)
            {
                chBoxÄVindruta.IsEnabled = false;
                chBoxÄVindruta.Content = "Komponent: vindruta saknas";
            }
            else
            {
                chBoxÄVindruta.IsEnabled = true;
                chBoxÄVindruta.Content = "Vindruta";
            }
            if (komponentSamling.komponentlista[0].Motorer < 1)
            {
                chBoxÄMotor.IsEnabled = false;
                chBoxÄMotor.Content = "Komponent: motor saknas";
            }
            else
            {
                chBoxÄMotor.IsEnabled = true;
                chBoxÄMotor.Content = "Motor";
            }

            if (komponentSamling.komponentlista[0].Karosser < 1)
            {
                chBoxÄKaross.IsEnabled = false;
                chBoxÄKaross.Content = "Komponent: kaross saknas";
            }
            else
            {
                chBoxÄKaross.IsEnabled = true;
                chBoxÄKaross.Content = "Kaross";
            }
        }

        private void fordonTypeMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
#endregion