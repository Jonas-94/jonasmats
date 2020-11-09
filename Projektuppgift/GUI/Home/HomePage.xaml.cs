using System;
using Logic.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Linq;
using System.Text.Json;
using System.IO;
using System.Windows.Forms;

namespace GUI.Home
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            
            try {
                FileStream fs = File.OpenRead(filePath);
                MekanikerSamling ms = new MekanikerSamling();
                string json = JsonSerializer.Serialize(ms);
                StreamReader sr = new StreamReader(fs);
                json = sr.ReadToEnd();
                MekanikerSamling uppLast = JsonSerializer.Deserialize<MekanikerSamling>(json);
                ms.mekaniker = uppLast.mekaniker;
                sr.Close();
                mekList = uppLast.mekaniker;
                foreach (var mek in uppLast.mekaniker)
                    mekDataGrid.Items.Add(mek);
                laddaFil.Background = Brushes.Green;
            }
            catch {
                laddaFil.Background = Brushes.Red;
                  }

        }
        public string filePath = "C:/Users/pc/Documents/GitHub/jonasmats/Projektuppgift/Logic/DAL/Mekaniker.json";
        Mekaniker mekaniker;
        List<Mekaniker> mekList = new List<Mekaniker>();
        MekanikerSamling ms = new MekanikerSamling();

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            mekaniker = new Mekaniker();
            mekaniker.Namn = txtBoxName.Text;
            mekaniker.Födelsedatum = txtBoxBirthday.Text;
            mekaniker.Anställningsdatum = txtBoxEmploymentDate.Text;
            mekaniker.Slutdatum = txtBoxUnEmploymentDate.Text;
            //mekList.Add(mekaniker);
            mekList.Add(mekaniker);

            ms.mekaniker = mekList;
            try
            {
                FileStream fs = File.OpenWrite(filePath);
                StreamWriter sw = new StreamWriter(fs);
                string json = JsonSerializer.Serialize(ms);
                sw.Write(json);
                sw.Close();
            }
            catch
            {
                FileDialog fileDialog = new OpenFileDialog();
                fileDialog.ShowDialog();
                filePath = fileDialog.FileName;
            }
            foreach (var mek in ms.mekaniker)
                mekDataGrid.Items.Remove(mek);
            foreach (var mek in ms.mekaniker)
                mekDataGrid.Items.Add(mek);
            txtBoxName.Text = "";
            txtBoxBirthday.Text = "";
            txtBoxEmploymentDate.Text = "";
            txtBoxUnEmploymentDate.Text = "";
        }

        private void lBoxMek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void editMekaniker_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void laddaFil_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FileDialog fileDialog = new OpenFileDialog();
                fileDialog.ShowDialog();
                filePath = fileDialog.FileName;

                FileStream fs = File.OpenRead(filePath);
                MekanikerSamling ms = new MekanikerSamling();
                string json = JsonSerializer.Serialize(ms);
                StreamReader sr = new StreamReader(fs);
                json = sr.ReadToEnd();
                MekanikerSamling uppLast = JsonSerializer.Deserialize<MekanikerSamling>(json);

                ms.mekaniker = uppLast.mekaniker;
                sr.Close();
                mekList = ms.mekaniker;
                    foreach (var mek in mekList)
                        mekDataGrid.Items.Remove(mek);

                    foreach (var mek in mekList)
                        mekDataGrid.Items.Add(mek);
                
                laddaFil.Background = Brushes.Green;
            }
            catch
            {

                laddaFil.Background = Brushes.Red;
            }

        }
    }
}
