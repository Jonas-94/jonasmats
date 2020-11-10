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
        FileLoader fLoader = new FileLoader();
        public string filePath = "C:/Users/pc/Documents/GitHub/jonasmats/Projektuppgift/Logic/DAL/Mekaniker.json";
        Mekaniker mekaniker;
        List<Mekaniker> mekList = new List<Mekaniker>();
        MekanikerSamling ms = new MekanikerSamling();
        public int mekListChoice { get; set; }
        public HomePage()
        {
            InitializeComponent();

            fLoader.LoadMechanicFile(filePath, mekList,ms, mekDataGrid, laddaFil);
        }

        

        private void BtnSaveMechanic_Click(object sender, RoutedEventArgs e)
        {
            CreateMechanic();


            ms.mekaniker.Add(mekaniker);


            // fLoader.WriteMechanicFile(filePath, ms);

            foreach (var mek in ms.mekaniker)
                mekDataGrid.Items.Remove(mek);
            foreach (var mek in ms.mekaniker)
                mekDataGrid.Items.Add(mek);

            ResetMechText();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void editMekaniker_Click(object sender, RoutedEventArgs e)
        {
            mekList = ms.mekaniker;
            int i = 0;
            for(i=0; i<mekList.Count;i++)
            {
                if (mekDataGrid.SelectedItem.ToString() == mekList[i].ToString())
                {
                    
                    chBoxBroms.IsChecked = false; chBoxKaross.IsChecked = false; chBoxMotor.IsChecked = false;
                    chBoxVindruta.IsChecked = false; chBoxDäck.IsChecked = false;
                    txtBoxName.Text = mekList[i].Namn;
                    txtBoxBirthday.Text = mekList[i].Fodelsedatum;
                    txtBoxEmploymentDate.Text = mekList[i].Anstallningsdatum;
                    txtBoxUnEmploymentDate.Text = mekList[i].Slutdatum;
                    if (mekList[i].Kbromsar == true)
                        chBoxBroms.IsChecked = true;
                    if (mekList[i].Kkaross == true)
                        chBoxKaross.IsChecked = true;
                    if (mekList[i].Kmotor == true)
                        chBoxMotor.IsChecked = true;
                    if (mekList[i].Kvindruta == true)
                        chBoxVindruta.IsChecked = true;
                    if (mekList[i].Kdack == true)
                        chBoxDäck.IsChecked = true;
                    mekListChoice = i;
                }
            }
           
            
        }

        private void laddaMekFil_Click(object sender, RoutedEventArgs e)
        {
            FileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            filePath = fileDialog.FileName;

            fLoader.LoadMechanicFile(filePath, mekList,ms, mekDataGrid, laddaFil);

        }

        private void btnSaveEditedMechanic_Click(object sender, RoutedEventArgs e)
        {
            
            mekList[mekListChoice].Namn = txtBoxName.Text;
            mekList[mekListChoice].Fodelsedatum = txtBoxBirthday.Text;
            mekList[mekListChoice].Anstallningsdatum = txtBoxEmploymentDate.Text;
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
                mekList[mekListChoice].Kdack = true;
            else
                mekList[mekListChoice].Kdack = false;

            foreach (var mek in mekList)
                mekDataGrid.Items.Remove(mek);

            foreach (var mek in mekList)
                mekDataGrid.Items.Add(mek);

            

            //fLoader.WriteMechanicFile(filePath, ms);

            ResetMechText();
            

        }

        public void CreateMechanic()
        {
            mekaniker = new Mekaniker();
            mekaniker.Namn = txtBoxName.Text;
            mekaniker.Fodelsedatum = txtBoxBirthday.Text;
            mekaniker.Anstallningsdatum = txtBoxEmploymentDate.Text;
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
                mekaniker.Kdack = true;
            else
                mekaniker.Kdack = false;

            
        }

        public void ResetMechText()
        {
            chBoxBroms.IsChecked = false; chBoxKaross.IsChecked = false; chBoxMotor.IsChecked = false;
            chBoxVindruta.IsChecked = false; chBoxDäck.IsChecked = false;
            txtBoxName.Text = "";
            txtBoxBirthday.Text = "";
            txtBoxEmploymentDate.Text = "";
            txtBoxUnEmploymentDate.Text = "";
        }

        private void saveMechFile_Click(object sender, RoutedEventArgs e)
        {
            fLoader.WriteMechanicFile(filePath, ms);


        }
    }
}
