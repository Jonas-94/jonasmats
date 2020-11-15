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
using System.Linq;

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
        public MekanikerSamling mekSamling = new MekanikerSamling();
        public FordonSamling fordonSamling = new FordonSamling();
        public int mekListChoice { get; set; }

        public BilSamling bilSamling { get; set; } = new BilSamling();
        public BussSamling bussSamling { get; set; } = new BussSamling();

        public UserSamling userSamling { get; set; } = new UserSamling();
        public InterfaceLoadSave IUser { get; set; } = new UserSamling();
        public InterfaceLoadSave IMekaniker { get; set; } = new MekanikerSamling();
        public InterfaceLoadSave IBil { get; set; } = new BilSamling();
        public InterfaceLoadSave IFordon { get; set; } = new FordonSamling();
        public InterfaceLoadSave IBuss { get; set; } = new BussSamling();
        
        
        public HomePage()
        {
            InitializeComponent();
            fordonTypeMenu.SelectedIndex = 0;
            try
            {
                fLoader.home = this;
                fLoader.folderPath = filePath;
                fLoader.LoadFiles("Alla");
                tb3fordonDataGrid.ItemsSource = fordonSamling.fordon;
                tb3mekDataGrid1.ItemsSource = mekSamling.mekaniker;
            }
            catch
            {
                System.Windows.MessageBox.Show("Öppna DAL-mappen för att ladda alllllllt");
            }
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
            int ID = 1;
            List<int> ids = new List<int>();
            ids.Clear();
            for (int i = 0; i < mekSamling.mekaniker.Count;i++) 
            {
                ids.Add(mekSamling.mekaniker[i].Id);
            }
            while(ids.Contains(ID))
                ID += 1;
            
            //Skapar ny mekaniker
            mekaniker = mekaniker.CreateMechanic(ID, txtBoxName.Text, txtBoxBirthday.Text,txtBoxEmploymentDate.Text,txtBoxUnEmploymentDate.Text,
                broms, kaross, motor, vindruta, däck);
            mekSamling.mekaniker.Add(mekaniker);
            //Refreshar DataGrid, nollställer och lägger in listan på nytt
            dataGridMekaniker.ItemsSource = null;
            dataGridMekaniker.ItemsSource = mekSamling.mekaniker;
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
            try
            {
                fLoader.LoadFiles("Alla");
            }
            catch { }
         }

        //metod som tar bort texten i TextBoxar och uncheckar Kompetens-bools i mekanikerfliken
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
            IMekaniker = mekSamling;
            IMekaniker.Save(filePath + "Mekaniker.json");
        }

        private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            
        }
        //Knapp som tar bort vald mekaniker
        private void btnDeleteMechanic_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < mekSamling.mekaniker.Count; i++)
                if (dataGridMekaniker.SelectedItem == mekSamling.mekaniker[i])
                    mekSamling.mekaniker.RemoveAt(i);
            dataGridMekaniker.ItemsSource = null;
            dataGridMekaniker.ItemsSource = mekSamling.mekaniker;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void writefile_Click(object sender, RoutedEventArgs e)
        {
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
            bool broms = false;
            bool kaross = false;
            bool motor = false;
            bool vindruta = false;
            bool däck = false;
            if (chBoxÄBroms.IsChecked == true) { broms = true; };
            if (chBoxÄKaross.IsChecked == true) { kaross = true; };
            if (chBoxÄMotor.IsChecked == true) { motor = true; };
            if (chBoxÄVindruta.IsChecked == true) { vindruta = true; };
            if (chBoxÄDäck.IsChecked == true) { däck = true; };

            if (fordonTypeMenu.Text == "Bil")
            {
                Bil bil = new Bil { Modellnamn = txtTb2modellNamn.Text, Registreringsnummer = txtTb2RegNr.Text, Registreringsdatum = txtTb2RegDate.Text,
                Äbromsar = broms,Äkaross = kaross,Ämotor = motor,Ävindruta = vindruta,Ädäck = däck};
                bilSamling.Bilar.Add(bil);
                IBil = bilSamling;
                IBil.Save(filePath + bilPath);
                fordonSamling.fordon.Add(bil);
            }
            else if(fordonTypeMenu.Text == "Buss")
            {
                Buss buss = new Buss { Modellnamn = txtTb2modellNamn.Text, Registreringsnummer = txtTb2RegNr.Text, Registreringsdatum = txtTb2RegDate.Text,
                    Äbromsar = broms,Äkaross = kaross, Ämotor = motor,Ävindruta = vindruta,Ädäck = däck
                };
                bussSamling.Bussar.Add(buss);
                IBuss = bussSamling;
                IBuss.Save(filePath + bussPath);
                fordonSamling.fordon.Add(buss);
            }
            
            dataGridFordon.ItemsSource = null;
            dataGridFordon.ItemsSource = fordonSamling.fordon;
            tb3fordonDataGrid.ItemsSource = fordonSamling.fordon;
         }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
        
        private void tb3mekDataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           

            

            

            
        }

        private void tb3btnMek_Click(object sender, RoutedEventArgs e)
        {
            FordonSamling fs = new FordonSamling();
            fs.fordon.Clear();
            tb3fordonDataGrid.ItemsSource = null;
            bool bromsar = false;
            bool kaross = false;
            bool motor = false;
            bool däck = false;
            bool vindruta = false;
            /*fordonSamling.fordon.Select(x => x.Äbromsar.Equals(bromsar) && x.Äkaross.Equals(bromsar) && x.Ämotor.Equals(bromsar)
               && x.Ädäck.Equals(bromsar) && x.Ävindruta.Equals(vindruta));*/
            for (int i = 0; i < mekSamling.mekaniker.Count; i++)
            {
                if (tb3mekDataGrid1.SelectedItem == mekSamling.mekaniker[i])
                {
                    if (mekSamling.mekaniker[i].Kbromsar == true)
                        bromsar = true;
                    if (mekSamling.mekaniker[i].Kkaross == true)
                        kaross = true;
                    if (mekSamling.mekaniker[i].Kmotor == true)
                        motor = true;
                    if (mekSamling.mekaniker[i].Kdäck == true)
                        däck = true;
                    if (mekSamling.mekaniker[i].Kvindruta == true)
                        vindruta = true;
                }
            }

            try
            {
                foreach (var f in fordonSamling.fordon)
                {
                    if (bromsar == true)
                    { 
                    fs.fordon.Add(f); 
                    } 
                    if (kaross == true && f.Äkaross ==true&& !fs.fordon.Contains(f))
                    {
                        fs.fordon.Add(f);
                    }
                    if (motor == true&&f.Ämotor==true&& !fs.fordon.Contains(f))
                    {
                       fs.fordon.Add(f); 
                    }
                    if (däck == true&& f.Ädäck==true&& !fs.fordon.Contains(f))
                    {
                        fs.fordon.Add(f); 
                    }
                    if (vindruta == true&&f.Ävindruta==true&& !fs.fordon.Contains(f))
                    {
                        fs.fordon.Add(f); 
                    }
                }

                tb3fordonDataGrid.ItemsSource = fs.fordon;
            }
            catch { }
        }
        public void getfordonärende<T>(List<T>list)
        {
        }
    }
}
