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

        string filePath { get; set; } = "C:/Users/pc/Documents/GitHub/jonasmats/Projektuppgift/Logic/DAL/";
        string userPath = "/User.json";
        string bussPath = "/Buss.json";string bilPath = "Bil.json";string mekPath = "Mekaniker.json";
        
        Mekaniker mekaniker;
        List<Mekaniker> mekList = new List<Mekaniker>();
        MekanikerSamling ms = new MekanikerSamling();
        FordonSamling fs = new FordonSamling();
        public int mekListChoice { get; set; }

        BilSamling bilSamling = new BilSamling();
        BussSamling bussSamling = new BussSamling();

        UserSamling userSamling = new UserSamling();
        InterfaceLoadSave IUser = new UserSamling();
        InterfaceLoadSave IMekaniker = new MekanikerSamling();
        InterfaceLoadSave IBil = new BilSamling();
        InterfaceLoadSave IFordon = new FordonSamling();
        InterfaceLoadSave IBuss = new BussSamling();
        
        
        public HomePage()
        {
            InitializeComponent();
            try
            {
                userSamling.users = IUser.Load<User>(filePath + userPath);
            }
            catch { }
            //Laddar mekaniker från Json
            ms.mekaniker = IMekaniker.Load<Mekaniker>(filePath+mekPath);
            dataGridMekaniker.ItemsSource = ms.mekaniker;
            bilSamling.Bilar = IBil.Load<Bil>(filePath+bilPath);
            dataGridFordon.ItemsSource = bilSamling.Bilar;
            

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
            dataGridMekaniker.ItemsSource = null;
            dataGridMekaniker.ItemsSource = ms.mekaniker;
            ResetMechText();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
        //Knapp som kan ladda Mekaniker-Json-fil om den inte hittats
        private void laddaMekFil_Click(object sender, RoutedEventArgs e)
        {
            fLoader.FoldPath();
            filePath = fLoader.folderPath;
             //FileDialog fileDialog = new OpenFileDialog();
             //fileDialog.ShowDialog();
             //filePath = fileDialog.FileName;
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
            //fLoader.SaveMechs(filePathMechanics, ms);
            //samlingar.AppendToString(ms.mekaniker);
            //ms.Save();

            IMekaniker = ms;
            IMekaniker.Save(filePath + "Mekaniker.json");
        }

        private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            
        }
        //Knapp som tar bort vald mekaniker
        private void btnDeleteMechanic_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < ms.mekaniker.Count; i++)
                if (dataGridMekaniker .SelectedItem == ms.mekaniker[i])
                    ms.mekaniker.RemoveAt(i);
            dataGridMekaniker.ItemsSource = null;
            dataGridMekaniker.ItemsSource = ms.mekaniker;
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
            
            userSamling.users.Add(user);
            IUser = userSamling;
            IUser.Save(filePath + userPath);
        }
        private void BtnCreateFordon_Click_2(object sender, RoutedEventArgs e)
        {
            if (fordonTypeMenu.Text == "Bil")
            {
                Bil bil = new Bil { Modellnamn = txtTb2modellNamn.Text, Registreringsnummer = txtTb2RegNr.Text, Registreringsdatum = txtTb2RegDate.Text };
                bilSamling.Bilar.Add(bil);
                IBil = bilSamling;
                IBil.Save(filePath + bilPath);
                fs.fordon.Add(bil);
            }
            else if(fordonTypeMenu.Text == "Buss")
            {
                Buss buss = new Buss { Modellnamn = txtTb2modellNamn.Text, Registreringsnummer = txtTb2RegNr.Text, Registreringsdatum = txtTb2RegDate.Text };
                bussSamling.Bussar.Add(buss);
                IBuss = bussSamling;
                IBuss.Save(filePath + bussPath);
                fs.fordon.Add(buss);
            }
            
            dataGridFordon.ItemsSource = null;
            dataGridFordon.ItemsSource = fs.fordon;
         }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
