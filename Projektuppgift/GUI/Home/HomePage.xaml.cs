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
        public int mekListChoice { get; set; }

        private void BtnSaveMechanic_Click(object sender, RoutedEventArgs e)
        {
            mekaniker = new Mekaniker();
            mekaniker.Namn = txtBoxName.Text;
            mekaniker.Födelsedatum = txtBoxBirthday.Text;
            mekaniker.Anställningsdatum = txtBoxEmploymentDate.Text;
            mekaniker.Slutdatum = txtBoxUnEmploymentDate.Text;
            if (chBoxBroms.IsChecked == true)
                mekaniker.Kbromsar = true;
            else
                mekaniker.Kbromsar = false;

            if (chBoxKaross.IsChecked == true)
                mekaniker.Kkaross = true;
            else
                mekaniker.Kkaross = false;

            if (chBoxMotor.IsChecked == true)
                mekaniker.Kmotor = true;
            else
                mekaniker.Kmotor = false;

            if (chBoxVindruta.IsChecked == true)
                mekaniker.Kvindruta = true;
            else
                mekaniker.Kvindruta = false;

            if (chBoxDäck.IsChecked == true)
                mekaniker.Kdäck = true;
            else
                mekaniker.Kdäck = false;

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
            chBoxBroms.IsChecked = false; chBoxKaross.IsChecked = false; chBoxMotor.IsChecked = false;
            chBoxVindruta.IsChecked = false; chBoxDäck.IsChecked = false;


        }

        private void lBoxMek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void editMekaniker_Click(object sender, RoutedEventArgs e)
        {

            txtBoxName.Text = mekDataGrid.SelectedItem.ToString();
            for(int i=0; i<mekList.Count;i++)
            {
                if (mekDataGrid.SelectedItem.ToString().Contains(mekList[i].ToString()))
                {
                    chBoxBroms.IsChecked = false; chBoxKaross.IsChecked = false; chBoxMotor.IsChecked = false;
                    chBoxVindruta.IsChecked = false; chBoxDäck.IsChecked = false;
                    txtBoxName.Text = mekList[i].Namn;
                    txtBoxBirthday.Text = mekList[i].Födelsedatum;
                    txtBoxEmploymentDate.Text = mekList[i].Anställningsdatum;
                    txtBoxUnEmploymentDate.Text = mekList[i].Slutdatum;
                    if (mekList[i].Kbromsar == true)
                        chBoxBroms.IsChecked = true;
                    if (mekList[i].Kkaross == true)
                        chBoxKaross.IsChecked = true;
                    if (mekList[i].Kmotor == true)
                        chBoxMotor.IsChecked = true;
                    if (mekList[i].Kvindruta == true)
                        chBoxVindruta.IsChecked = true;
                    if (mekList[i].Kdäck == true)
                        chBoxDäck.IsChecked = true;
                    mekListChoice = i;
                }
            }
           
            
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
                    foreach (var rmek in mekList)
                        mekDataGrid.Items.Remove(rmek);
                    foreach (var mek in mekList)
                        mekDataGrid.Items.Add(mek);
                laddaFil.Background = Brushes.Green;
            }
            catch
            {
                laddaFil.Background = Brushes.Red;
            }

        }

        private void btnSaveEditedMechanic_Click(object sender, RoutedEventArgs e)
        {
            
            mekList[mekListChoice].Namn = txtBoxName.Text;
            mekList[mekListChoice].Födelsedatum = txtBoxBirthday.Text;
            mekList[mekListChoice].Anställningsdatum = txtBoxEmploymentDate.Text;
            mekList[mekListChoice].Slutdatum = txtBoxUnEmploymentDate.Text;

            if (chBoxBroms.IsChecked == true)
                mekList[mekListChoice].Kbromsar = true;
            else
                mekList[mekListChoice].Kbromsar = false;

            if (chBoxKaross.IsChecked == true)
                mekList[mekListChoice].Kkaross = true;
            else
                mekList[mekListChoice].Kkaross = false;

            if (chBoxMotor.IsChecked == true)
                mekList[mekListChoice].Kmotor = true;
            else
                mekList[mekListChoice].Kmotor = false;

            if (chBoxVindruta.IsChecked == true)
                mekList[mekListChoice].Kvindruta = true;
            else
                mekList[mekListChoice].Kvindruta = false;

            if (chBoxDäck.IsChecked == true)
                mekList[mekListChoice].Kdäck = true;
            else
                mekList[mekListChoice].Kdäck = false;

            foreach (var mek in mekList)
                mekDataGrid.Items.Remove(mek);

            foreach (var mek in mekList)
                mekDataGrid.Items.Add(mek);

            SaveFile();

            chBoxBroms.IsChecked = false; chBoxKaross.IsChecked = false; chBoxMotor.IsChecked = false;
            chBoxVindruta.IsChecked = false; chBoxDäck.IsChecked = false;
            txtBoxName.Text = "";
            txtBoxBirthday.Text = "";
            txtBoxEmploymentDate.Text = "";
            txtBoxUnEmploymentDate.Text = "";

        }

        private void rBtnBroms_Checked(object sender, RoutedEventArgs e)
        {

        }

        public void SaveFile()
        {
            
                ms.mekaniker = mekList;
                FileStream fs = File.OpenWrite(filePath);
                StreamWriter sw = new StreamWriter(fs);
                string json = JsonSerializer.Serialize(ms);
                sw.Write(json);
                sw.Close();
            
           
        }
    }
}
