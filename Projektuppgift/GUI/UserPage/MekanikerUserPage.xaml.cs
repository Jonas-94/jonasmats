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
namespace GUI.Home
{
    /// <summary>
    /// Interaction logic for MekanikerUserPage.xaml
    /// </summary>
    public partial class MekanikerUserPage : Page
    {
        FileLoader fLoader = new FileLoader();
        ÄrendeSamling ärendeSamling = new ÄrendeSamling();
        BilSamling bilSamling = new BilSamling();
        LastbilSamling lbSamling = new LastbilSamling();
        BussSamling bussSamling = new BussSamling();
        MotorcykelSamling mcSamling = new MotorcykelSamling();
        MekanikerSamling mekSamling = new MekanikerSamling();
        public int id;
        string missingFile { get; set; }
        public MekanikerUserPage()
        {
            InitializeComponent();
            LoadAllFiles();
            var mek = mekSamling.mekaniker.Where(x => x.Id == fLoader.Id);
            Welcome.Content = $"Välkommen {mek.Select(x => x.förnamn)}";
            pågåendeärenden.ItemsSource = ärendeSamling.ärenden.Where(ä => ä.ÄrendeID == fLoader.Id);
            färdigärenden.ItemsSource = ärendeSamling.ärenden.Where(f => f.ÄrendeID == fLoader.Id);
        }

        public void LoadAllFiles()
        {

            try
            {
                //fLoader.LoadBilar(bilSamling);
            }
            catch (Exception a) { if (a.Source != null) missingFile += "\n" + a.Message; }
            try
            {
               // fLoader.LoadBussar(bussSamling);
            }
            catch (Exception b) { if (b.Source != null) missingFile += "\n" + b.Message; }
            try
            {
                fLoader.LoadLastbilar();
            }
            catch (Exception c) { if (c.Source != null) missingFile += "\n" + c.Message; }
            try
            {
               // fLoader.LoadMotorcyklar(mcSamling);
            }
            catch (Exception d) { if (d.Source != null) missingFile += "\n" + d.Message; }
            try
            {
                fLoader.LoadÄrenden();
            }
            catch (Exception e) { if (e.Source != null) missingFile += "\n" + e.Message; }
            try
            {
                fLoader.LoadMekaniker();
            }
            catch (Exception f) { if (f.Source != null) missingFile += "\n" + f.Message; }
        }
    }
}
