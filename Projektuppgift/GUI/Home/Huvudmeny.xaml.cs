using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Logic.DAL;
namespace GUI.Home
{
    /// <summary>
    /// Interaction logic for Huvudmeny.xaml
    /// </summary>
    public partial class Huvudmeny : Page
    {
        FileLoader fLoader = new FileLoader();
        
        public Huvudmeny()
        {
            InitializeComponent();
            
        }
        public void FindFolderPath()
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.ShowDialog();
            fLoader.FindFolderPath(folder.SelectedPath);
            
        }
        private void MekanikerMeny_Click(object sender, RoutedEventArgs e)
        {
            MekanikerPage mPage = new MekanikerPage();
            this.NavigationService.Navigate(mPage);
        }

        private void FordonMeny_Click(object sender, RoutedEventArgs e)
        {
            FordonPage fPage = new FordonPage();
            this.NavigationService.Navigate(fPage);
        }

        private void SkapaÄrendeMeny_Click(object sender, RoutedEventArgs e)
        {
            SkapaÄrendePage säPage = new SkapaÄrendePage();
            this.NavigationService.Navigate(säPage);
        }

        private void ÄrendeMeny_Click(object sender, RoutedEventArgs e)
        {
            ÄrendePage äPage = new ÄrendePage();
            this.NavigationService.Navigate(äPage);
        }

        private void KomponentMeny_Click(object sender, RoutedEventArgs e)
        {
            KomponenterPage kPage = new KomponenterPage();
            this.NavigationService.Navigate(kPage);
        }
    }
}
