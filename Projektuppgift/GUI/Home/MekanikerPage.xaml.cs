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
using System.Windows.Forms;

namespace GUI.Home
{
    /// <summary>
    /// Interaction logic for MekanikerPage.xaml
    /// </summary>
    public partial class MekanikerPage : Page
    {
        FileLoader fLoader = new FileLoader();
        FolderPath fPath;
        string folderPath { get; set; }
        MekanikerSamling mekanikerSamling { get; set; } = new MekanikerSamling();
        public MekanikerPage()
        {
            InitializeComponent();
            SparaRedigeradMekaniker.Visibility = Visibility.Hidden;
            try
            {
                fLoader.LoadMekaniker();
                MekanikerGrid.ItemsSource = fLoader.mekSamling.mekaniker;
            }
            catch { }
        }
        private void Skapa_Mekaniker_Click(object sender, RoutedEventArgs e)
        {
            SkapaMekaniker();
        }
        public string FirstLetterCapital(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            char[] letters = str.ToCharArray();
            foreach (var l in letters)
            {
                char.ToLower(l);
            }

            letters[0] = char.ToUpper(letters[0]);
            return new string(letters);
        }
        public void SkapaMekaniker()
        {
            //bools för kompetenser
            bool broms = false;
            bool kaross = false;
            bool motor = false;
            bool vindruta = false;
            bool däck = false;
            if (Broms.IsChecked == true) { broms = true; };
            if (Kaross.IsChecked == true) { kaross = true; };
            if (Motor.IsChecked == true) { motor = true; };
            if (Vindruta.IsChecked == true) { vindruta = true; };
            if (Däck.IsChecked == true) { däck = true; };
            int nyID = 1;
            try
            {
                List<int> idlist = new List<int>();
                foreach (var i in fLoader.idSamling.idlista)
                    idlist.Add(i.IDnr);
                nyID = idlist.Max();
            }
            catch { }
            bool run = true;
            while (run)
            {
                string _förnamn = FirstLetterCapital(Förnamn.Text);
                string _efternamn = FirstLetterCapital(Efternamn.Text);
                string _födelsedatum = "";
                string _anställningsdatum = "";
                string _slutdatum = "";
                if (string.IsNullOrEmpty(Förnamn.Text))
                {
                    System.Windows.Forms.MessageBox.Show("Fyll i ett namn.");
                }
                if (DateTime.TryParse(Födelsedatum.Text, out DateTime bDay))
                {
                    _födelsedatum = bDay.ToShortDateString();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Fyll i ett giltigt födelsedatum (YYYY-MM-DD)");
                    break;
                }
                if (DateTime.TryParse(Anställningsdatum.Text, out DateTime aDay))
                    _anställningsdatum = aDay.ToShortDateString();
                else
                {
                    System.Windows.Forms.MessageBox.Show("Fyll i ett giltigt anställningsdatum (YYYY-MM-DD)");
                    break;
                }
                if (DateTime.TryParse(Slutdatum.Text, out DateTime uDay))
                {
                    _slutdatum = uDay.ToShortDateString();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Fyll i ett giltigt slutdatum (YYYY-MM-DD)");
                    break;
                }
                while (nyID == 0 || fLoader.mekSamling.mekaniker.Exists(m => m.Id.Equals(nyID)))
                {
                    nyID += 1;
                }
                //Skapar ny mekaniker
                Mekaniker mekaniker = new Mekaniker
                {
                    Id = nyID,
                    förnamn = _förnamn,
                    efternamn = _efternamn,
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
                fLoader.idSamling.idlista.Add(id);
                fLoader.mekSamling.mekaniker.Add(mekaniker);
                //Refreshar DataGrid, nollställer och lägger in listan på nytt
                MekanikerGrid.ItemsSource = null;
                MekanikerGrid.ItemsSource = fLoader.mekSamling.mekaniker;
                fLoader.SaveMekaniker();
                fLoader.SaveID(fLoader.idSamling);
                break;
            }
        }
        private void MekanikerGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Mekaniker m = (Mekaniker)MekanikerGrid.SelectedItem;
                if (m != null)
                    Mekanikertext.Content = m.ToStringBeskrivning();
            }
            catch { }
        }
        int IDnr { get; set; }
        private void btnRedigeraMekaniker_Click(object sender, RoutedEventArgs e)
        {
            SparaRedigeradMekaniker.Visibility = Visibility.Visible;
            btnSkapaMekaniker.Visibility = Visibility.Hidden;
            Mekaniker m = (Mekaniker)MekanikerGrid.SelectedItem;
            IDnr = m.Id;
            Förnamn.Text = m.förnamn;
            Efternamn.Text = m.efternamn;
            Födelsedatum.Text = m.Födelsedatum;
            Anställningsdatum.Text = m.Anställningsdatum;
            Slutdatum.Text = m.Slutdatum;
            if (m.Kbromsar == true) { Broms.IsChecked = true; };
            if (m.Kkaross == true) { Kaross.IsChecked = true; };
            if (m.Kvindruta == true) { Vindruta.IsChecked = true; };
            if (m.Kmotor == true) { Motor.IsChecked = true; };
            if (m.Kdäck == true) { Däck.IsChecked = true; };

            btnSkapaMekaniker.Visibility = Visibility.Hidden;
            btnRedigeraMekaniker.Visibility = Visibility.Hidden;
        }

        private void SparaRedigeradMekaniker_Click(object sender, RoutedEventArgs e)
        {
            SparaRedigeradMekaniker.Visibility = Visibility.Hidden;
            //bools för kompetenser
            bool broms = false;
            bool kaross = false;
            bool motor = false;
            bool vindruta = false;
            bool däck = false;
            if (Broms.IsChecked == true) { broms = true; };
            if (Kaross.IsChecked == true) { kaross = true; };
            if (Motor.IsChecked == true) { motor = true; };
            if (Vindruta.IsChecked == true) { vindruta = true; };
            if (Däck.IsChecked == true) { däck = true; };
            

            bool run = true;
            while (run)
            {
                string _förnamn = FirstLetterCapital(Förnamn.Text);
                string _efternamn = FirstLetterCapital(Efternamn.Text);
                string _födelsedatum = "";
                string _anställningsdatum = "";
                string _slutdatum = "";
                if (string.IsNullOrEmpty(Förnamn.Text))
                {
                    System.Windows.Forms.MessageBox.Show("Fyll i ett namn.");
                }
                if (DateTime.TryParse(Födelsedatum.Text, out DateTime bDay))
                {
                    _födelsedatum = bDay.ToShortDateString();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Fyll i ett giltigt födelsedatum (YYYY-MM-DD)");
                    break;
                }
                if (DateTime.TryParse(Anställningsdatum.Text, out DateTime aDay))
                    _anställningsdatum = aDay.ToShortDateString();
                else
                {
                    System.Windows.Forms.MessageBox.Show("Fyll i ett giltigt anställningsdatum (YYYY-MM-DD)");
                    break;
                }
                if (DateTime.TryParse(Slutdatum.Text, out DateTime uDay))
                {
                    _slutdatum = uDay.ToShortDateString();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Fyll i ett giltigt slutdatum (YYYY-MM-DD)");
                    break;
                }

                //Skapar ny mekaniker
                Mekaniker mekaniker = new Mekaniker
                {
                    Id = IDnr,
                    förnamn = _förnamn,
                    efternamn = _efternamn,
                    Födelsedatum = _födelsedatum,
                    Anställningsdatum = _anställningsdatum,
                    Slutdatum = _slutdatum,
                    Kbromsar = broms,
                    Kkaross = kaross,
                    Kmotor = motor,
                    Kvindruta = vindruta,
                    Kdäck = däck
                };
                int index = fLoader.mekSamling.mekaniker.FindIndex(x => x.Id.Equals(IDnr));
                fLoader.mekSamling.mekaniker[index] = mekaniker;
                //Refreshar DataGrid, nollställer och lägger in listan på nytt
                MekanikerGrid.ItemsSource = null;
                MekanikerGrid.ItemsSource = fLoader.mekSamling.mekaniker;
                fLoader.SaveMekaniker();
                fLoader.SaveID(fLoader.idSamling);
                break;
            }
        }

        private void btnHuvudmeny_Click(object sender, RoutedEventArgs e)
        {
            Huvudmeny huvudmeny = new Huvudmeny();
            this.NavigationService.Navigate(huvudmeny);
        }
    }
}