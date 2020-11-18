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
using System.Threading.Tasks;

namespace GUI.Home
{
    public class FileLoader
    {

        public string folderPath { get; set; } = "C:/Users/pc/Documents/GitHub/jonasmats/Projektuppgift/Logic/DAL/";

        string userPath = "User.json"; string fordonPath = "Fordon.json"; string lastbilPath = "Lastbilar.json";
        string bussPath = "Buss.json"; string bilPath = "Bil.json"; string motorcykelPath = "Motorcyklar.json";
        string mekPath = "Mekaniker.json"; string ärendePath = "Ärenden.json";

        public InterfaceLoadSave IUser { get; set; } = new UserSamling();
        public InterfaceLoadSave IMekaniker { get; set; } = new MekanikerSamling();
        public InterfaceLoadSave IBil { get; set; } = new BilSamling();
        public InterfaceLoadSave IFordon { get; set; } = new FordonSamling();
        public InterfaceLoadSave IBuss { get; set; } = new BussSamling();
        public InterfaceLoadSave ILastbil { get; set; } = new LastbilSamling();
        public InterfaceLoadSave IMotorcyklar { get; set; } = new MotorcykelSamling();
        public InterfaceLoadSave IÄrende { get; set; } = new ÄrendeSamling();
        public MekanikerSamling mekSamling { get; set; } = new MekanikerSamling();
        public FordonSamling fordonSamling { get; set; } = new FordonSamling();
        public ÄrendeSamling ärendeSamling { get; set; } = new ÄrendeSamling();
        public MotorcykelSamling motorcykelSamling { get; set; } = new MotorcykelSamling();
        public BilSamling bilSamling { get; set; } = new BilSamling();
        public BussSamling bussSamling { get; set; } = new BussSamling();
        public UserSamling userSamling { get; set; } = new UserSamling();
        public LastbilSamling lastbilSamling { get; set; } = new LastbilSamling();


        public HomePage home { get; set; }
        
        public void FindFolderPath()
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.ShowDialog();
            folderPath = folder.SelectedPath;
        }
        public void SaveUser()
        {
            IUser = home.userSamling;
            IUser.SaveAsync(folderPath + userPath);
        }
        public void SaveMekaniker()
        {
            IMekaniker = home.mekSamling;
            IMekaniker.SaveAsync(folderPath + mekPath);
        }
        public void SaveLastBilar()
        {
            ILastbil = home.lastbilSamling;
            ILastbil.SaveAsync(folderPath + lastbilPath);
        }
        public void SaveBilar()
        {
            IBil = home.bilSamling;
            IBil.SaveAsync(folderPath + bilPath);
        }
        public void SaveBussar()
        {
            IBuss = home.bussSamling;
            IBuss.SaveAsync(folderPath + bussPath);
        }
        public void SaveMotorcyklar()
        {
            IMotorcyklar = home.motorcykelSamling;
            IMotorcyklar.SaveAsync(folderPath + motorcykelPath);
        }
        public async  void SaveÄrenden()
        {
            IÄrende = home.ärendeSamling;
            IÄrende.SaveAsync(folderPath + ärendePath);
        }
        public void SaveAllFordon()
        {
            SaveBilar();
            SaveBussar();
            SaveLastBilar();
            SaveMotorcyklar();
        }
        public void FordonReload()
        {
            home.fordonSamling.fordon.Clear();
            home.fordonSamling.AddFordon<Bil>(home.bilSamling.Bilar);
            home.fordonSamling.AddFordon<Buss>(home.bussSamling.Bussar);
            home.fordonSamling.AddFordon<Lastbil>(home.lastbilSamling.lastbilar);
            home.fordonSamling.AddFordon<Motorcykel>(home.motorcykelSamling.motorcyklar);
        }
        
        public void LoadFiles(string choices)
        {
           
            if (choices == "Alla")
                    choices = "User Mekaniker Bilar Bussar Lastbilar Motorcyklar";
                if (choices.Contains("User"))
                {
                     home.userSamling.users = IUser.Load<User>(folderPath + userPath);
                }
                if (choices.Contains("Mekaniker"))
                {
                    home.mekSamling.mekaniker = IMekaniker.Load<Mekaniker>(folderPath + mekPath);
                }
                if (choices.Contains("Bilar"))
                {
                    home.bilSamling.Bilar = IBil.Load<Bil>(folderPath + bilPath);
                    home.fordonSamling.AddFordon<Bil>(home.bilSamling.Bilar);
                }
                if (choices == "Bussar")
                {
                    home.bussSamling.Bussar = IBuss.Load<Buss>(folderPath + bussPath);
                    home.fordonSamling.AddFordon<Buss>(home.bussSamling.Bussar);
                }
                if(choices == "Lastbilar")
                {
                home.lastbilSamling.lastbilar = ILastbil.Load<Lastbil>(folderPath + lastbilPath);
                home.fordonSamling.AddFordon<Lastbil>(home.lastbilSamling.lastbilar);
                }
                if(choices == "Motorcyklar")
                {
                home.motorcykelSamling.motorcyklar = IMotorcyklar.Load<Motorcykel>(folderPath + motorcykelPath);
                home.fordonSamling.AddFordon<Motorcykel>(home.motorcykelSamling.motorcyklar);
                }
                if(choices == "Ärenden")
                {
                home.ärendeSamling.ärenden = IÄrende.Load<Ärende>(folderPath + ärendePath);
                }
        }
        
    }
}