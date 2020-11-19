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
    /// Interaction logic for ÄrendePage.xaml
    /// </summary>
    public partial class ÄrendePage : Page
    {
        FileLoader fLoader { get; set; } = new FileLoader();
        public ÄrendePage()
        {
            InitializeComponent();
            fLoader.LoadÄrenden();
            fLoader.LoadMekaniker();
            PågåendeÄrendenGrid.ItemsSource =
                fLoader.ärendeSamling.ärenden.Select(x => x.ÄrendeStatus == false);
            FärdigaÄrendenGrid.ItemsSource =
                fLoader.ärendeSamling.ärenden.Select(x => x.ÄrendeStatus == true);
        }

        private void btnÄrendeFärdigt_Click(object sender, RoutedEventArgs e)
        {
            Ärende  ä = (Ärende)PågåendeÄrendenGrid.SelectedItem;
            int index = fLoader.ärendeSamling.ärenden.IndexOf(ä);
            Mekaniker m = fLoader.ärendeSamling.ärenden[index].mekaniker;
            int indexmek = fLoader.mekSamling.mekaniker.IndexOf(m);
            if(fLoader.mekSamling.mekaniker[indexmek].Ärenden > 0)
            fLoader.mekSamling.mekaniker[indexmek].Ärenden -= 1;
        }
    }
}
