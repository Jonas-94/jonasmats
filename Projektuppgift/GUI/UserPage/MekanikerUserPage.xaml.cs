using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Logic.DAL;
using System.Linq;
using Logic.Entities;
namespace GUI.Home
{
    /// <summary>
    /// Interaction logic for MekanikerUserPage.xaml
    /// </summary>
    public partial class MekanikerUserPage : Page
    {

        FileLoader fLoader = new FileLoader();

        int id { get; set; }
        int index { get; set; }
        string missingFile { get; set; }
        public MekanikerUserPage()
        {
            InitializeComponent();
            try
            { fLoader.LoadÄrenden(); }catch { }
            try { fLoader.LoadMekaniker(); } catch { }
            try { fLoader.LoadFordon(); } catch { }
            id = fLoader.SendID();
            foreach (var mek in fLoader.mekSamling.mekaniker)
                if (id == mek.Id)
                {
                    index = fLoader.mekSamling.mekaniker.IndexOf(mek);
                }
            Kompetenser();
            Welcome.Content = $"Välkommen {fLoader.mekSamling.mekaniker[index].förnamn}";
            RefreshGrids();
        }
        public void RefreshGrids()
        {
            pågåendeÄrenden.ItemsSource = null;
            färdigaÄrenden.ItemsSource = null;
            pågåendeÄrenden.ItemsSource = fLoader.ärendeSamling.ärenden.Where(ä => ä.ÄrendeID == id && ä.ÄrendeStatus == false);
            färdigaÄrenden.ItemsSource = fLoader.ärendeSamling.ärenden.Where(f => f.ÄrendeID == id && f.ÄrendeStatus == true);
        }

        private void btnSparaKompetens_Click(object sender, RoutedEventArgs e)
        {
            if(kvindruta.IsChecked == true) { fLoader.mekSamling.mekaniker[index].Kvindruta = true; }
            else { fLoader.mekSamling.mekaniker[index].Kvindruta = true; }
            if (kdäck.IsChecked == true) { fLoader.mekSamling.mekaniker[index].Kdäck = true; }
            else { fLoader.mekSamling.mekaniker[index].Kbromsar = true; }
            if (kbroms.IsChecked == true) { fLoader.mekSamling.mekaniker[index].Kbromsar = true; }
            else { fLoader.mekSamling.mekaniker[index].Kkaross = true; }
            if (kkaross.IsChecked == true) { fLoader.mekSamling.mekaniker[index].Kkaross = true; }
            if(kmotor.IsChecked == true) { fLoader.mekSamling.mekaniker[index].Kmotor = true; }
            else { fLoader.mekSamling.mekaniker[index].Kmotor = true; }
            fLoader.SaveMekaniker();
        }

        private void Kompetenser()
        {
            if(fLoader.mekSamling.mekaniker[index].Kdäck == true) { kdäck.IsChecked = true; }
            if (fLoader.mekSamling.mekaniker[index].Kbromsar == true){ kbroms.IsChecked = true; }
            if (fLoader.mekSamling.mekaniker[index].Kkaross == true) { kkaross.IsChecked = true; }
            if(fLoader.mekSamling.mekaniker[index].Kmotor == true) { kmotor.IsChecked = true; }
            if (fLoader.mekSamling.mekaniker[index].Kvindruta == true){ kvindruta.IsChecked = true; }
        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string fbeskrivning = "";
                if (pågåendeÄrenden.SelectedItem is Ärende)
                {
                    Ärende ä = (Ärende)pågåendeÄrenden.SelectedItem;
                    Bil b = new Bil();
                    Lastbil lb = new Lastbil();
                    Buss bb = new Buss();
                    Motorcykel mc = new Motorcykel();
                    foreach (var x in fLoader.bilSamling.Bilar)
                    {
                        if (ä.RegNr == x.Registreringsnummer)
                            fbeskrivning = x.ToStringBeskrivning();
                    }
                    foreach (var x in fLoader.lastbilSamling.lastbilar)
                    {
                        if (ä.RegNr == x.Registreringsnummer)
                            fbeskrivning = x.ToStringBeskrivning();
                    }
                    foreach (var x in fLoader.motorcykelSamling.motorcyklar)
                    {
                        if (ä.RegNr == x.Registreringsnummer)
                            fbeskrivning = x.ToStringBeskrivning();
                    }
                    foreach (var x in fLoader.bussSamling.Bussar)
                    {
                        if (ä.RegNr == x.Registreringsnummer)
                            fbeskrivning = x.ToStringBeskrivning();
                    }
                    lblÄrendeBeskrivning.Content = ä.Beskrivning + "\n\n" + fbeskrivning;
                }
            }
            catch { }
        }

        private void btnFärdigtÄrende_Click(object sender, RoutedEventArgs e)
        {
            fLoader.LoadMekaniker();
            Ärende ä = (Ärende)pågåendeÄrenden.SelectedItem;
            int äindex = fLoader.ärendeSamling.ärenden.IndexOf(ä);
            if (pågåendeÄrenden.SelectedItem is Ärende)
            {
                foreach (var m in fLoader.mekSamling.mekaniker)
                {
                    if (ä.ÄrendeID == m.Id && m.Ärenden > 0)
                        m.Ärenden -= 1;
                }
                foreach (var x in fLoader.bilSamling.Bilar)
                {
                    if (ä.RegNr == x.Registreringsnummer)
                        x.ÄrendeKlart = true;
                }
                foreach (var x in fLoader.lastbilSamling.lastbilar)
                {
                    if (ä.RegNr == x.Registreringsnummer)
                        x.ÄrendeKlart = true;
                }
                foreach (var x in fLoader.motorcykelSamling.motorcyklar)
                {
                    if (ä.RegNr == x.Registreringsnummer)
                        x.ÄrendeKlart = true;
                }
                foreach (var x in fLoader.bussSamling.Bussar)
                {
                    if (ä.RegNr == x.Registreringsnummer)
                        x.ÄrendeKlart = true;
                }
                foreach (var x in fLoader.ärendeSamling.ärenden)
                {
                    if (ä.RegNr == x.RegNr)
                    {
                        x.ÄrendeStatus = true;
                    }
                }
                fLoader.SaveMekaniker();
                fLoader.SaveAllFordon();
                fLoader.SaveÄrenden();
                RefreshGrids();
            }
        }
        private void färdigaÄrenden_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string fbeskrivning = "";
                if (färdigaÄrenden.SelectedItem is Ärende)
                {
                    Ärende ä = (Ärende)färdigaÄrenden.SelectedItem;
                    Bil b = new Bil();
                    Lastbil lb = new Lastbil();
                    Buss bb = new Buss();
                    Motorcykel mc = new Motorcykel();
                    foreach (var x in fLoader.bilSamling.Bilar)
                    {
                        if (ä.RegNr == x.Registreringsnummer)
                            fbeskrivning = x.ToStringBeskrivning();
                    }
                    foreach (var x in fLoader.lastbilSamling.lastbilar)
                    {
                        if (ä.RegNr == x.Registreringsnummer)
                            fbeskrivning = x.ToStringBeskrivning();
                    }
                    foreach (var x in fLoader.motorcykelSamling.motorcyklar)
                    {
                        if (ä.RegNr == x.Registreringsnummer)
                            fbeskrivning = x.ToStringBeskrivning();
                    }
                    foreach (var x in fLoader.bussSamling.Bussar)
                    {
                        if (ä.RegNr == x.Registreringsnummer)
                            fbeskrivning = x.ToStringBeskrivning();
                    }
                    lblÄrendeBeskrivning.Content = ä.Beskrivning + "\n\n" + fbeskrivning;
                }
            }
            catch { }
        }
    }
}
