using Logic.DAL;
using Logic.Entities;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
namespace GUI.Home
{
    /// <summary>
    /// Interaction logic for ÄrendePage.xaml
    /// </summary>
    public partial class ÄrendePage : Page
    {
        FileLoader fLoader { get; set; } = new FileLoader();
        Ärende ä { get; set; } = new Ärende();
        public ÄrendePage()
        {
            InitializeComponent();
            try
            {
                fLoader.LoadFordon();
            }
            catch { fLoader.SaveAllFordon(); }
            fLoader.FordonReload();
            try
            {
                fLoader.LoadMekaniker();
            }
            catch { fLoader.SaveMekaniker(); }
            try
            {
                fLoader.LoadÄrenden();
            }
            catch { fLoader.SaveÄrenden(); }
            //RefreshÄrenden();
            RefreshGrid();
        }

        private void btnÄrendeFärdigt_Click(object sender, RoutedEventArgs e)
        {
            fLoader.LoadMekaniker();
            Ärende ä = (Ärende)PågåendeÄrendenGrid.SelectedItem;
            int äindex = fLoader.ärendeSamling.ärenden.IndexOf(ä);
            if (PågåendeÄrendenGrid.SelectedItem is Ärende)
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
                foreach(var x in fLoader.ärendeSamling.ärenden)
                {
                    if(ä.RegNr == x.RegNr)
                    {
                        x.ÄrendeStatus = true;
                    }
                }
            }
            fLoader.SaveMekaniker();
            fLoader.SaveAllFordon();
            fLoader.SaveÄrenden();
            RefreshGrid();
        }
        private void RefreshGrid()
        {
            PågåendeÄrendenGrid.ItemsSource = null;
            FärdigaÄrendenGrid.ItemsSource = null;
            PågåendeÄrendenGrid.ItemsSource =
                fLoader.ärendeSamling.ärenden.Where(x => x.ÄrendeStatus == false);
            FärdigaÄrendenGrid.ItemsSource =
                fLoader.ärendeSamling.ärenden.Where(x => x.ÄrendeStatus == true);
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
        private void SelectionChanged()
        {
            
        }
        private void PågåendeÄrendenGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string fbeskrivning = "";
                if (PågåendeÄrendenGrid.SelectedItem is Ärende)
                {
                    Ärende ä = (Ärende)PågåendeÄrendenGrid.SelectedItem;
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

        private void btnHuvudmeny_Click(object sender, RoutedEventArgs e)
        {
            Huvudmeny hMeny = new Huvudmeny();
            this.NavigationService.Navigate(hMeny);
        }

        private void FärdigaÄrendenGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string fbeskrivning = "";
                if (FärdigaÄrendenGrid.SelectedItem is Ärende)
                {
                    Ärende ä = (Ärende)FärdigaÄrendenGrid.SelectedItem;
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
