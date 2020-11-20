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
        int index = 0;
        string missingFile { get; set; }
        public MekanikerUserPage()
        {
            InitializeComponent();
            try
            {
                fLoader.LoadÄrenden();
                
            }
            catch { }
            try { fLoader.LoadMekaniker(); } catch { }
            try { fLoader.LoadFordon(); } catch { }
            id = fLoader.SendID();
            foreach (var mek in fLoader.mekSamling.mekaniker)
                if (id == mek.Id)
                {
                    index = fLoader.mekSamling.mekaniker.IndexOf(mek);
                }
            Welcome.Content = $"Välkommen {fLoader.mekSamling.mekaniker[index].förnamn}";
            RefreshGrids();
        }
        public void RefreshGrids()
        {
            pågåendeÄrenden.ItemsSource = fLoader.ärendeSamling.ärenden.Where(ä => ä.ÄrendeID == id && ä.ÄrendeStatus == false);
            färdigaÄrenden.ItemsSource = fLoader.ärendeSamling.ärenden.Where(f => f.ÄrendeID == id && f.ÄrendeStatus == true);
        }

        private void btnSparaKompetens_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnFärdigtÄrende_Click(object sender, RoutedEventArgs e)
        {
            fLoader.LoadMekaniker();
            Ärende ä = (Ärende)pågåendeÄrenden.SelectedItem;
            int äindex = fLoader.ärendeSamling.ärenden.IndexOf(ä);
            if (pågåendeÄrenden.SelectedItem is Ärende)
            {
                fLoader.ärendeSamling.ärenden[äindex].ÄrendeStatus = true;
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
            }
        }
    }
}
