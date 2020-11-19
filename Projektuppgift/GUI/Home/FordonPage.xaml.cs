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
using Logic.Entities;
using System.Threading.Tasks;
namespace GUI.Home
{
    /// <summary>
    /// Interaction logic for FordonMeny.xaml
    /// </summary>

    public partial class FordonPage : Page
    {
        FileLoader fLoader = new FileLoader();
        public FordonPage()
        {
            InitializeComponent();
            try
            {
                fLoader.LoadBilar();
                fLoader.LoadLastbilar();
                fLoader.LoadMekaniker();
                FordonGrid.ItemsSource = fLoader.lastbilSamling.lastbilar;
            }
            catch { }

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
        
        public void SkapaFordon()
        {
            bool broms = false;
            bool kaross = false;
            bool motor = false;
            bool vindruta = false;
            bool däck = false;
            bool dragkrok = false;
            if (Bromsar.IsChecked == true) { broms = true; };
            if (Kaross.IsChecked == true) { kaross = true; };
            if (Motor.IsChecked == true) { motor = true; };
            if (Vindruta.IsChecked == true) { vindruta = true; };
            if (Däck.IsChecked == true) { däck = true; };
            if (Dragkrok.IsChecked == true) { dragkrok = true; };

            string drivmedel = "";
            if (Drivmedel.SelectedItem == Bensin)
                drivmedel = "Bensin";
            else if (Drivmedel.SelectedItem == Etanol)
                drivmedel = "Etanol";
            else if (Drivmedel.SelectedItem == El)
                drivmedel = "El";
            else if (Drivmedel.SelectedItem == Diesel)
                drivmedel = "Diesel";

            bool run = true;
            while (run)
            {
                if (broms == false && kaross == false && motor == false && vindruta == false && däck == false)
                {
                    System.Windows.Forms.MessageBox.Show("Välj ett eller flera ärenden.");
                }
                int maxviktOrPassagerare;
                int milmätare = 0;
                string modellNamn = FirstLetterCapital(txtModellnamn.Text);
                string regNr = txtRegNr.Text.ToUpper();
                DateTime datum;
                string regDatum;
                if (DateTime.TryParse(txtRegDatum.Text, out datum))
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
                foreach (var f in fLoader.fordonSamling.fordon)
                {
                    if (f.Registreringsnummer == regNr)
                        System.Windows.Forms.MessageBox.Show("Registreringsnumret finns redan i registret.");
                    break;
                }
                if (int.TryParse(txtMilmätare.Text, out int mätare))
                    milmätare = mätare;
                else
                    System.Windows.Forms.MessageBox.Show("Fyll i milmätare (i siffror)");

                if (rbtnBil.IsChecked == true)
                {
                    string _biltyp = "";
                    if (Biltyp.SelectedItem == Sedan)
                        _biltyp = "Sedan";
                    else if (Biltyp.SelectedItem == Herrgårdsvagn)
                        _biltyp = "Herrgårdsvagn";
                    else if (Biltyp.SelectedItem == Cabriolet)
                        _biltyp = "Cabriolet";
                    else if (Biltyp.SelectedItem == Halvkombi)
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
                    fLoader.bilSamling.Bilar.Add(bil);
                    fLoader.fordonSamling.fordon.Add(bil);
                    fLoader.SaveBilar();
                }
                else if (rbtnBuss.IsChecked == true)
                {
                    if (!string.IsNullOrEmpty(txtMaxvikt_Passagerare.Text) && Int32.TryParse(txtMaxvikt_Passagerare.Text, out int siffra))
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
                    fLoader.bussSamling.Bussar.Add(buss);
                    fLoader.fordonSamling.fordon.Add(buss);
                    fLoader.SaveBussar();

                }
                else if (rbtnLastbil.IsChecked == true)
                {
                    if (!string.IsNullOrEmpty(txtMaxvikt_Passagerare.Text) && Int32.TryParse(txtMaxvikt_Passagerare.Text, out int siffra))
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
                    fLoader.lastbilSamling.lastbilar.Add(lastbil);
                    fLoader.SaveLastBilar();
                }
                else if (rbtnMc.IsChecked == true)
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
                    fLoader.motorcykelSamling.motorcyklar.Add(motorcykel);
                    fLoader.SaveMotorcyklar();
                }
                fLoader.FordonReload();
                FordonGrid.ItemsSource = null;
                FordonGrid.ItemsSource = fLoader.fordonSamling.fordon;
                run = false;
            }
        }
        private async void rbtnBil_Checked(object sender, RoutedEventArgs e)
        {
            await Task.Delay(25);
            if (rbtnBil.IsChecked == true)
            {
                Dragkrok.Visibility = Visibility.Visible;
                Biltyp.Visibility = Visibility.Visible;
                txtMaxvikt_Passagerare.Visibility = Visibility.Hidden;
                lblMaxvikt_Passagerare.Visibility = Visibility.Hidden;
            }
        }
        private void rbtnLastbil_Checked(object sender, RoutedEventArgs e)
        {
            if (rbtnLastbil.IsChecked == true)
            {
                Dragkrok.Visibility = Visibility.Hidden;
                Biltyp.Visibility = Visibility.Hidden;
                txtMaxvikt_Passagerare.Visibility = Visibility.Visible;
                lblMaxvikt_Passagerare.Visibility = Visibility.Visible;
                lblMaxvikt_Passagerare.Content = "Maxvikt:";
            }
        }
        private void rbtnBuss_Checked(object sender, RoutedEventArgs e)
        {
            if (rbtnBuss.IsChecked == true)
            {
                txtMaxvikt_Passagerare.Visibility = Visibility.Visible;
                lblMaxvikt_Passagerare.Visibility = Visibility.Visible;
                lblMaxvikt_Passagerare.Content = "Antal passagerare:";
                Dragkrok.Visibility = Visibility.Hidden;
                Biltyp.Visibility = Visibility.Hidden;
            }
        }
        private void rbtnMc_Checked(object sender, RoutedEventArgs e)
        {
            if (rbtnMc.IsChecked == true)
            {
                txtMaxvikt_Passagerare.Visibility = Visibility.Hidden;
                lblMaxvikt_Passagerare.Visibility = Visibility.Hidden;
                lblMaxvikt_Passagerare.Content = "Antal passagerare:";
                Dragkrok.Visibility = Visibility.Hidden;
                Biltyp.Visibility = Visibility.Hidden;
            }
        }
        private void btnSkapaFordon_Click(object sender, RoutedEventArgs e)
        {
            SkapaFordon();
        }
        private void FordonGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Bil b = new Bil();
            Lastbil lb = new Lastbil();
            Buss bb = new Buss() ;
            Motorcykel mc = new Motorcykel();
            if (FordonGrid.SelectedItem is Bil)
            {
                b = (Bil)FordonGrid.SelectedItem;
                lblFordonBeskrivning.Content = b.ToStringBeskrivning();
            }
            else if (FordonGrid.SelectedItem is Lastbil)
            {
                lb = (Lastbil)FordonGrid.SelectedItem;
                lblFordonBeskrivning.Content = lb.ToStringBeskrivning();
            }
            else if (FordonGrid.SelectedItem is Buss)
            {
                bb = (Buss)FordonGrid.SelectedItem;
                lblFordonBeskrivning.Content = bb.ToStringBeskrivning();
            }
            else if (FordonGrid.SelectedItem is Motorcykel)
            {
                mc = (Motorcykel)FordonGrid.SelectedItem;
                lblFordonBeskrivning.Content = mc.ToStringBeskrivning();
            }
        }

        private void btnRedigeraFordon_Click(object sender, RoutedEventArgs e)
        {
           
        }
        public void RedigeraFordon()
        {

        }
    }
}
