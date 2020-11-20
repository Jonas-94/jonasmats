using Logic.DAL;
using Logic.Entities;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Linq;
namespace GUI.Home
{
    /// <summary>
    /// Interaction logic for SkapaÄrendePage.xaml
    /// </summary>
    public partial class SkapaÄrendePage : Page
    {
        FileLoader fLoader { get; set; } = new FileLoader();
        string missingFile { get; set; }
        Fordon f { get; set; }
        Bil b { get; set; }
        Lastbil lb { get; set; }
        Buss bb { get; set; }
        Motorcykel mc { get; set; }
        Mekaniker mek { get; set; }
        string beskrivning { get; set; } = "";
        Ärende ärende { get; set; }
        public SkapaÄrendePage()
        {
            InitializeComponent();

            LoadFiles();
            if (missingFile != null)
            {
                System.Windows.Forms.MessageBox.Show("Json-filer skapas automatiskt.\n" +
                    $"Följande filer hittades inte:\n{missingFile}");
            }
            RefreshGrid();

        }
        public void RefreshGrid()
        {
            fLoader.FordonReload();
            FordonGrid.ItemsSource =
            FordonGrid.ItemsSource =
                fLoader.fordonSamling.fordon.Where(x => x.ÄrendeTaget == false);
        }
        private void btnVäljFordon_Click(object sender, RoutedEventArgs e)
        {
            f = (Fordon)FordonGrid.SelectedItem;
            beskrivning = BeskrivningsMetod(f);
            if (f != null)
            {
                foreach (var fordon in fLoader.fordonSamling.fordon)
                {
                    if (fordon.Registreringsnummer == f.Registreringsnummer)
                        beskrivning = BeskrivningsMetod(fordon);
                }
            }
            VisaMekanikerTillFordon(f);
            btnVäljFordon.IsEnabled = false;
            lblFordonBeskrivning.Content = beskrivning;
        }
        private void btnVäljMekaniker_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mek = (Mekaniker)MekGrid.SelectedItem;
            }
            catch { }

            btnVäljMekaniker.IsEnabled = false;
            if (btnVäljMekaniker.IsEnabled == false && btnVäljFordon.IsEnabled == false)
                btnSkapaÄrende.Background = Brushes.Green;
        }
        private void btnSkapaÄrende_Click(object sender, RoutedEventArgs e)
        {
            bool run = true;
            while (run)
            {
                if (f != null && mek != null)
                {
                    int index = fLoader.mekSamling.mekaniker.IndexOf(mek);
                    if (mek.Ärenden >= 2)
                    {
                        System.Windows.Forms.MessageBox.Show("Meckarn har för många ärenden");
                        break;
                    }
                    else
                        fLoader.mekSamling.mekaniker[index].Ärenden += 1;
                    if (f is Bil)
                        f = f as Bil;
                    else if (f is Lastbil)
                        f = f as Lastbil;
                    else if (f is Buss)
                        f = f as Buss;
                    else if (f is Motorcykel)
                        f = f as Motorcykel;
                            
                    ärende = new Ärende
                    {
                        RegDatum = f.Registreringsdatum,
                        ÄrendeID = mek.Id,
                        förnamn = mek.förnamn,
                        efternamn = mek.efternamn,
                        RegNr = f.Registreringsnummer,
                        Beskrivning = beskrivning,
                        fordon = f,
                        mekaniker = fLoader.mekSamling.mekaniker[index]
                    };
                    fLoader.SaveMekaniker();
                    fLoader.ärendeSamling.ärenden.Add(ärende);
                    fLoader.SaveÄrenden();
                    int fIndex;
                    if (f is Bil)
                    {
                        fIndex = fLoader.bilSamling.Bilar.IndexOf(f as Bil);
                        fLoader.bilSamling.Bilar[fIndex].ÄrendeTaget = true;
                        fLoader.bilSamling.Bilar[fIndex].Id = mek.Id;
                    }
                    if (f is Lastbil)
                    {
                        fIndex = fLoader.lastbilSamling.lastbilar.IndexOf(f as Lastbil);
                        fLoader.lastbilSamling.lastbilar[fIndex].ÄrendeTaget = true;
                        fLoader.lastbilSamling.lastbilar[fIndex].Id = mek.Id;
                    }
                    if (f is Buss)
                    {
                        fIndex = fLoader.bussSamling.Bussar.IndexOf(f as Buss);
                        fLoader.bussSamling.Bussar[fIndex].ÄrendeTaget = true;
                        fLoader.bussSamling.Bussar[fIndex].Id = mek.Id;
                    }
                    if (f is Motorcykel)
                    {
                        fIndex = fLoader.motorcykelSamling.motorcyklar.IndexOf(f as Motorcykel);
                        fLoader.motorcykelSamling.motorcyklar[fIndex].ÄrendeTaget = true;
                        fLoader.bussSamling.Bussar[fIndex].Id = mek.Id;
                    }
                    fLoader.SaveAllFordon();
                    
                    System.Windows.Forms.MessageBox.Show($"Mekanikern {mek.förnamn} {mek.efternamn}" +
                        $" tog ärendet för fordonet {f.Modellnamn} {f.Registreringsnummer}");
                }
                VisaMekanikerTillFordon(f);
                mek = null;
                f = null;
                RefreshGrid();
                btnVäljMekaniker.IsEnabled = true;
                btnVäljFordon.IsEnabled = true;
                btnSkapaÄrende.Background = Brushes.LightGray;
                run = false;
            }
        }

        private void HuvudMeny_Click(object sender, RoutedEventArgs e)
        {
            Huvudmeny hMeny = new Huvudmeny();
            this.NavigationService.Navigate(hMeny);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FordonTyp.SelectedItem == AllaFordon)
            {
                FordonGrid.ItemsSource = null;
                FordonGrid.ItemsSource = fLoader.fordonSamling.fordon;
            }
            if (FordonTyp.SelectedItem == Bilar)
            {
                FordonGrid.ItemsSource = null;
                FordonGrid.ItemsSource = fLoader.bilSamling.Bilar;
            }
            else if (FordonTyp.SelectedItem == Lastbilar)
            {
                FordonGrid.ItemsSource = null;
                FordonGrid.ItemsSource = fLoader.lastbilSamling.lastbilar;
            }
            else if (FordonTyp.SelectedItem == Bussar)
            {
                FordonGrid.ItemsSource = null;
                FordonGrid.ItemsSource = fLoader.bussSamling.Bussar;
            }
            else if (FordonTyp.SelectedItem == Motorcyklar)
            {
                FordonGrid.ItemsSource = null;
                FordonGrid.ItemsSource = fLoader.motorcykelSamling.motorcyklar;
            }

        }
        public string BeskrivningsMetod(Fordon fordon)
        {

            string Beskrivning = " ";
            bool broms = fordon.Äbromsar;
            bool kaross = fordon.Äkaross;
            bool motor = fordon.Ämotor;
            bool vindruta = fordon.Ävindruta;
            bool däck = fordon.Ädäck;
            string bromsstring = "", karossstring = "", motorstring = "", vindrutastring = "", däckstring = "";
            if (broms == true)
                bromsstring = "\nBehandling av bromsar";
            if (kaross == true)
                karossstring = "\nBehanding av kaross";
            if (motor == true)
                motorstring = "\nBehandling av motor";
            if (vindruta == true)
                vindrutastring = "\nBehandling av vindruta";
            if (däck == true)
                däckstring = "\nBehandling av däck";
            Beskrivning = " " + bromsstring + karossstring + motorstring + vindrutastring + däckstring;
            return Beskrivning;
        }
        public void VisaMekanikerTillFordon(Fordon fordo)
        {
            List<bool> ärendeBools = new List<bool>();
            ärendeBools.Clear();
            MekanikerSamling ms = new MekanikerSamling();
            MekGrid.ItemsSource = null;

            ärendeBools.Add(fordo.Äbromsar);
            ärendeBools.Add(fordo.Äkaross);
            ärendeBools.Add(fordo.Ämotor);
            ärendeBools.Add(fordo.Ädäck);
            ärendeBools.Add(fordo.Ävindruta);
            bool run = true;
            while (run)
            {
                for (int i = 0; i < fLoader.mekSamling.mekaniker.Count; i++)
                {
                    if (ärendeBools[0] == true && fLoader.mekSamling.mekaniker[i].Kbromsar == false)
                        continue;
                    else if (ärendeBools[1] == true && fLoader.mekSamling.mekaniker[i].Kkaross == false)
                        continue;
                    else if (ärendeBools[2] == true && fLoader.mekSamling.mekaniker[i].Kmotor == false)
                        continue;
                    else if (ärendeBools[3] == true && fLoader.mekSamling.mekaniker[i].Kdäck == false)
                        continue;
                    else if (ärendeBools[4] == true && fLoader.mekSamling.mekaniker[i].Kvindruta == false)
                        continue;
                    else
                        ms.mekaniker.Add(fLoader.mekSamling.mekaniker[i]);
                }
                run = false;
            }
            MekGrid.ItemsSource = null;
            MekGrid.ItemsSource = ms.mekaniker;
        }
        public void LoadFiles()
        {
            try
            {
                fLoader.LoadBilar();
            }
            catch (Exception a) { if (a.Source != null) missingFile += "\n" + a.Message; }
            try
            {
                fLoader.LoadBussar();
            }
            catch (Exception b) { if (b.Source != null) missingFile += "\n" + b.Message; }
            try
            {
                fLoader.LoadLastbilar();
            }
            catch (Exception c) { if (c.Source != null) missingFile += "\n" + c.Message; }
            try
            {
                fLoader.LoadMotorcyklar();
            }
            catch (Exception d) { if (d.Source != null) missingFile += "\n" + d.Message; }
            try
            {
                fLoader.LoadMekaniker();
            }
            catch (Exception g) { if (g.Source != null) missingFile += "\n" + g.Message; }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            f = null;
            mek = null;
            btnSkapaÄrende.Background = Brushes.LightGray;
            btnVäljFordon.IsEnabled = true;
            btnVäljMekaniker.IsEnabled = true;
        }
    }

}
