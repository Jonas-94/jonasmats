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
using Logic.DAL;

namespace GUI.Home
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        FileLoader fLoader = new FileLoader();
        public string filePath = "C:/Users/pc/Documents/GitHub/jonasmats/Projektuppgift/Logic/DAL/";
        public string filePathMechanics = "C:/Users/pc/Documents/GitHub/jonasmats/Projektuppgift/Logic/DAL/Mekaniker.json";
        public string filePathFordons = "C:/Users/pc/Documents/GitHub/jonasmats/Projektuppgift/Logic/DAL/Fordon.json";
        Mekaniker mekaniker;
        List<Mekaniker> mekList = new List<Mekaniker>();
        MekanikerSamling ms = new MekanikerSamling();
        public int mekListChoice { get; set; }

        BilSamling bs = new BilSamling();
        Samlingar samlingar = new Samlingar();

        public HomePage()
        {
            InitializeComponent();
            //Laddar mekaniker från Json
            fLoader.GetMechs(filePathMechanics, ms);
            //Lägger in mekaniker i Datagrid
            mekDataGrid.ItemsSource = ms.mekaniker;
            /* --- Test för fordonsdatagrid ---
            //Lägger in 2 nya bilar i Klassen bilsamling(ofärdigt)
            bs.Bilar.Add(new Bil { Dragkrok = true, Drivmedel = "El", Däck = 4 });
            bs.Bilar.Add(new Bil { Dragkrok = false, Drivmedel = "OK", Däck = 3 });
            //Lägger in bilar i datagrid
            fordonDataGrid.ItemsSource = bs.Bilar;
            //sparar bilar i Json()
            fLoader.WriteFordonFile(filePathFordons, bs.Bilar);
            */

            
        }

        //Knappen för att lägga till en ny Mekaniker
        private void BtnCreateMechanic_Click(object sender, RoutedEventArgs e)
        {
            Mekaniker mekaniker = new Mekaniker();
            //bools för kompetenser
            bool broms = false;
            bool kaross = false;
            bool motor = false;
            bool vindruta = false;
            bool däck = false;
            if (chBoxBroms.IsChecked == true) { broms = true; };
            if (chBoxKaross.IsChecked == true) { kaross = true; };
            if(chBoxMotor.IsChecked == true) { motor = true; };
            if(chBoxVindruta.IsChecked == true) { vindruta = true; };
            if (chBoxDäck.IsChecked == true){ däck = true; };
            //Skapar ny mekaniker
            mekaniker = mekaniker.CreateMechanic(txtBoxName.Text, txtBoxBirthday.Text,txtBoxEmploymentDate.Text,txtBoxUnEmploymentDate.Text,
                broms, kaross, motor, vindruta, däck);
            ms.mekaniker.Add(mekaniker);
            //Refreshar DataGrid, nollställer och lägger in listan på nytt
            mekDataGrid.ItemsSource = null;
            mekDataGrid.ItemsSource = ms.mekaniker;
            ResetMechText();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
        //Knapp som kan ladda Mekaniker-Json-fil om den inte hittats
        private void laddaMekFil_Click(object sender, RoutedEventArgs e)
        {
            FileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            filePathMechanics = fileDialog.FileName;
            //fLoader.LoadFile(filePathMechanics, ms);
        }

        //metod som tar bort texten i TextBoxar och uncheckar Kompetens-bools i mekanikerfliker
        public void ResetMechText()
        {
            chBoxBroms.IsChecked = false; chBoxKaross.IsChecked = false; chBoxMotor.IsChecked = false;
            chBoxVindruta.IsChecked = false; chBoxDäck.IsChecked = false;
            txtBoxName.Text = "";
            txtBoxBirthday.Text = "";
            txtBoxEmploymentDate.Text = "";
            txtBoxUnEmploymentDate.Text = "";
        }
        //metod som spara mekaniker till json
        private void saveMechFile_Click(object sender, RoutedEventArgs e)
        {
            ms.mekaniker = ms.mekaniker;
            //string write = fLoader.AppendToString(ms.mekaniker);
            //fLoader.WriteFile(filePathMechanics, ms.mekaniker);
            fLoader.SaveMechs(filePathMechanics, ms);
            //samlingar.AppendToString(ms.mekaniker);
        }

        private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            
        }
        //Knapp som tar bort vald mekaniker
        private void btnDeleteMechanic_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < ms.mekaniker.Count; i++)
                if (mekDataGrid.SelectedItem == ms.mekaniker[i])
                    ms.mekaniker.RemoveAt(i);
            mekDataGrid.ItemsSource = null;
            mekDataGrid.ItemsSource = ms.mekaniker;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            txtBoxAnvändare.Text = fLoader.AppendToString(ms.mekaniker);
            
        }

        private void writefile_Click(object sender, RoutedEventArgs e)
        {
            MekanikerSamling ms = new MekanikerSamling();
            txtBoxAnvändare.Text = JsonSerializer.Serialize(ms.mekaniker);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            User user = new User {Username = txtBoxCreateUsername.Text, Password = txtBoxCreatePassword.Text };
            UserSamling us = new UserSamling();
            us.users.Add(user);
            fLoader.SaveUserJson(filePath + "User.Json", us);
            
           
        }
    }
}
