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
namespace GUI.Home
{
    /// <summary>
    /// Interaction logic for KomponenterPage.xaml
    /// </summary>
    public partial class KomponenterPage : Page
    {
        FileLoader fl = new FileLoader();
        BilKomponenter bkomp = new BilKomponenter();
        LastbilKomponenter lbkomp = new LastbilKomponenter();
        BussKomponenter bbkomp = new BussKomponenter();
        McKomponenter mckomp = new McKomponenter();
        public KomponenterPage()
        {
            InitializeComponent();
            try
            {
                fl.LoadBilKomp();
            }
            catch { }
            try
            {
                fl.LoadBussKomp();
            }
            catch { }
            try
            {
                fl.LoadLastbilKomp();
            }
            catch { }
            try
            {
                fl.LoadMcKomp();
            }
            catch { }
            RefreshKomponenter();
        }
        public void RefreshKomponenter()
        {

            if (fl.kompBilSamling.komp.Count == 0)
                fl.kompBilSamling.komp.Add(bkomp);
            if (fl.kompBussSamling.komp.Count == 0)
                fl.kompBussSamling.komp.Add(bbkomp);
            if (fl.kompLastBSamling.komp.Count == 0)
                fl.kompLastBSamling.komp.Add(lbkomp);
            if (fl.kompMcSamling.komp.Count == 0)
                fl.kompMcSamling.komp.Add(mckomp);
            try
            {
                lblBilDäck.Content = fl.kompBilSamling.komp[0].Däck.ToString();
                lblBilKaross.Content = fl.kompBilSamling.komp[0].Karosser.ToString();
                lblBilMotor.Content = fl.kompBilSamling.komp[0].Motorer.ToString();
                lblBilbroms.Content = fl.kompBilSamling.komp[0].Bromsar.ToString();
                lblBilVindRuta.Content = fl.kompBilSamling.komp[0].Vindrutor.ToString();
            }
            catch { }
            try
            {
                lblLastbilBroms.Content = fl.kompLastBSamling.komp[0].Bromsar.ToString();
                lblLastbilDäck.Content = fl.kompLastBSamling.komp[0].Däck.ToString();
                lblLastbilMotor.Content = fl.kompLastBSamling.komp[0].Motorer.ToString();
                lblLastbilVindruta.Content = fl.kompLastBSamling.komp[0].Vindrutor.ToString();
                lblLastbilKaross.Content = fl.kompLastBSamling.komp[0].Karosser.ToString();
            }
            catch { }
            try
            {
                lblMcBroms.Content = fl.kompMcSamling.komp[0].Bromsar.ToString();
                lblMcVindruta.Content = fl.kompMcSamling.komp[0].Vindrutor.ToString();
                lblMcMotor.Content = fl.kompMcSamling.komp[0].Motorer.ToString();
                lblMcKaross.Content = fl.kompMcSamling.komp[0].Karosser.ToString();
                lblMcDäck.Content = fl.kompMcSamling.komp[0].Däck.ToString();
            }
            catch { }
            try
            {
                lblBussBroms.Content = fl.kompBussSamling.komp[0].Bromsar.ToString();
                lblBussDäck.Content = fl.kompBussSamling.komp[0].Däck.ToString();
                lblBussKaross.Content = fl.kompBussSamling.komp[0].Karosser.ToString();
                lblBussVindruta.Content = fl.kompBussSamling.komp[0].Vindrutor.ToString();
                lblBussMotor.Content = fl.kompBussSamling.komp[0].Motorer.ToString();
            }
            catch { }

        }
        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void SaveKomponenter ()
        {
            fl.SaveBilKomp();
            fl.SaveLastbilKomp();
            fl.SaveBussKomp();
            fl.SaveMcKomp();
        }

        private void btnAddKomponenter_Click(object sender, RoutedEventArgs e)
        {
            int bildäck, bilkaross, bilbroms, bilvindruta;
            int lbdäck, lbkaross, lbbroms, lbvindruta, lbmotor;
            int bussdäck, busskaross, bussbroms, bussvindruta, bussmotor;
            int mcdäck, mckaross, mcbroms, mcvindruta, mcmotor;
            int.TryParse(txtBildäck.Text, out bildäck);
            int.TryParse(txtBilkaross.Text, out bilkaross);
            int.TryParse(txtBilbroms.Text, out bilbroms);
            int.TryParse(txtBilVindruta.Text, out bilvindruta);
            int.TryParse(txtBilmotor.Text, out int bilmotor);
            int.TryParse(txtLastbilDäck.Text, out lbdäck);
            int.TryParse(txtLastbilKaross.Text, out lbkaross);
            int.TryParse(txtLastbilBroms.Text, out lbbroms);
            int.TryParse(txtLastbilVindruta.Text, out lbvindruta);
            int.TryParse(txtLastbilMotor.Text, out lbmotor);
            int.TryParse(txtBussDäck.Text, out bussdäck);
            int.TryParse(txtBussKaross.Text, out busskaross);
            int.TryParse(txtBussBroms.Text, out bussbroms);
            int.TryParse(txtBussVindruta.Text, out bussvindruta);
            int.TryParse(txtBussMotor.Text, out bussmotor);
            int.TryParse(txtMcDäck.Text, out mcdäck);
            int.TryParse(txtMcKaross.Text, out mckaross);
            int.TryParse(txtMcBroms.Text, out mcbroms);
            int.TryParse(txtMcVindruta.Text, out mcvindruta);
            int.TryParse(txtMcMotor.Text, out mcmotor);

            txtBildäck.Text = "";
            txtBilkaross.Text = "";
            txtBilbroms.Text = "";
            txtBilVindruta.Text = "";
            txtBilmotor.Text = "";
            txtLastbilDäck.Text = "";
            txtLastbilKaross.Text = "";
            txtLastbilBroms.Text = "";
            txtLastbilVindruta.Text = "";
            txtLastbilMotor.Text = "";
            txtBussDäck.Text = "";
            txtBussKaross.Text = "";
            txtBussBroms.Text = "";
            txtBussVindruta.Text = "";
            txtBussMotor.Text = "";
            txtMcDäck.Text = "";
            txtMcKaross.Text = "";
            txtMcBroms.Text = "";
            txtMcVindruta.Text = "";
            txtMcMotor.Text = "";

            fl.kompBilSamling.komp[0].Däck += bildäck;
            fl.kompBilSamling.komp[0].Karosser += bilkaross;
            fl.kompBilSamling.komp[0].Motorer += bilbroms;
            fl.kompBilSamling.komp[0].Bromsar += bilvindruta;
            fl.kompBilSamling.komp[0].Vindrutor += bilmotor;
            fl.kompLastBSamling.komp[0].Bromsar += lbbroms;
            fl.kompLastBSamling.komp[0].Däck += lbdäck;
            fl.kompLastBSamling.komp[0].Motorer += lbmotor;
            fl.kompLastBSamling.komp[0].Vindrutor += lbvindruta;
            fl.kompLastBSamling.komp[0].Karosser += lbkaross;
            fl.kompMcSamling.komp[0].Bromsar += mcbroms;
            fl.kompMcSamling.komp[0].Vindrutor += mcvindruta;
            fl.kompMcSamling.komp[0].Motorer += mcmotor;
            fl.kompMcSamling.komp[0].Karosser += mckaross;
            fl.kompMcSamling.komp[0].Däck += mcdäck;
            fl.kompBussSamling.komp[0].Bromsar += bussbroms;
            fl.kompBussSamling.komp[0].Däck += bussdäck;
            fl.kompBussSamling.komp[0].Karosser += busskaross;
            fl.kompBussSamling.komp[0].Vindrutor += bussvindruta;
            fl.kompBussSamling.komp[0].Motorer += bussmotor;



            RefreshKomponenter();
            SaveKomponenter();
        }
    }
}
