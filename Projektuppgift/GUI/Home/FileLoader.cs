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
using Logic.Entities;

namespace GUI.Home
{
    public class FileLoader
    {
        public HomePage home { get; set; }
        public string folderPath { get; set; }

        string userPath = "/User.json";
        string bussPath = "/Buss.json"; string bilPath = "Bil.json"; string mekPath = "Mekaniker.json";

        
        
        public void FoldPath()
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.ShowDialog();
            folderPath = folder.SelectedPath;
        }

        public void LoadFiles(string choices)
        {
           
            if (choices == "Alla")
                    choices = "User Mekaniker Bilar Bussar";
                if (choices.Contains("User"))
                {
                    home.userSamling.users = home.IUser.Load<User>(folderPath + userPath);
                }
                if (choices.Contains("Mekaniker"))
                {
                    home.mekSamling.mekaniker = home.IMekaniker.Load<Mekaniker>(folderPath + mekPath);
                    home.dataGridMekaniker.ItemsSource = home.mekSamling.mekaniker;
                }
                if (choices.Contains("Bilar"))
                {
                    home.bilSamling.Bilar = home.IBil.Load<Bil>(folderPath + bilPath);
                    home.fordonSamling.AddFordon<Bil>(home.bilSamling.Bilar);
                    home.dataGridFordon.ItemsSource = home.fordonSamling.fordon;
                }
                if (choices == "Bussar")
                {
                    home.bussSamling.Bussar = home.IBuss.Load<Buss>(folderPath + bussPath);
                    home.fordonSamling.AddFordon<Buss>(home.bussSamling.Bussar);
                    home.dataGridFordon.ItemsSource = home.fordonSamling.fordon;
                }
            
        }
        
    }
}
