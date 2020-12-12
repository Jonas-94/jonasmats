using GUI.Home;
using Logic.Services;
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
using Logic.Entities;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;
using Logic.DAL;
using System.Threading.Tasks;
using System.Linq;
namespace GUI.Login
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    ///
    public partial class LoginPage : Page
    {
        FileLoader fLoader = new FileLoader();
        private const string _errorMsg = "Inloggningen misslyckades";
        Regex reg = new Regex();
        string missingFile { get; set; }
        

        private LoginService _loginService;
        public LoginPage()
        {
            InitializeComponent();
            _loginService = new LoginService();

            tbUsernam.Text = "Bosse";
            pbPassword.Password = "Meckarn123";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            string username = tbUsernam.Text;
            string password = pbPassword.Password;
            bool regexSuccessful = reg.checkForEmail(username);
            
                bool successful = _loginService.Login(username, password);
            
            if (successful && username == "Bosse")
            {
                Huvudmeny hMeny = new Huvudmeny();
                this.NavigationService.Navigate(hMeny);
            }
            else if(regexSuccessful && successful)
            {
               
                MekanikerUserPage mekPage = new MekanikerUserPage();
                this.NavigationService.Navigate(mekPage);
            }
                else
            {

                System.Windows.MessageBox.Show(_errorMsg);
                this.tbUsernam.Clear();
                this.pbPassword.Clear();
                }
            }
            catch { System.Windows.Forms.MessageBox.Show("Ladda DAL-mappen / User-filen"); }
        }
        private async void btnfindUserFile_Click(object sender, RoutedEventArgs e)
        {
            FindFolderPath();
            await LoadAllFiles();
            //await fLoader.SaveAll();
           
            if (missingFile != null)
                System.Windows.Forms.MessageBox.Show($"Json-filer skapas automatiskt. \nFöljande filer skapades:\n\n{missingFile}");
        }
        public void FindFolderPath()
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.ShowDialog();
            //string filePath = folder.SelectedPath + "/User.json";
            //_loginService.filePathGet = filePath;
            fLoader.FindFolderPath(folder.SelectedPath);
            
        }
       
        public async Task LoadAllFiles()
        {
            
            try
            {
                fLoader.LoadBilar();
            }
            catch (Exception a) { if (a.Source != null) await fLoader.SaveBilar(); missingFile += "\n" + a.Message; }
            try
            {
                fLoader.LoadBussar();
            }
            catch (Exception b) { if (b.Source != null) await fLoader.SaveBussar(); missingFile += "\n" + b.Message; }
            try
            {
                fLoader.LoadLastbilar();
            }
            catch (Exception c) { if (c.Source != null) await fLoader.SaveLastBilar(); missingFile += "\n" + c.Message; }
            try
            {
                fLoader.LoadMotorcyklar();
            }
            catch (Exception d) { if (d.Source != null) await fLoader.SaveMotorcyklar(); missingFile += "\n" + d.Message; }
            try
            {
                fLoader.LoadÄrenden();
            }
            catch (Exception e) { if (e.Source != null) await fLoader.SaveÄrenden(); missingFile += "\n" + e.Message; }
            try
            {
                fLoader.LoadUsers();
            }
            catch (Exception f) { if (f.Source != null) await fLoader.SaveUser(); missingFile += "\n" + f.Message; }
            try
            {
                fLoader.LoadMekaniker();
            }
            catch (Exception g) { if (g.Source != null) await fLoader.SaveMekaniker(); missingFile += "\n" + g.Message; }
            try
            {
                fLoader.LoadID();
            }
            catch (Exception h) { if (h.Source != null) await fLoader.SaveID(); missingFile += "\n" + h.Message; }
            try
            {
                fLoader.LoadLastbilKomp();
            }
            catch (Exception i) { if (i.Source != null) await fLoader.SaveLastbilKomp(); missingFile += "\n" + i.Message; }
            try
            {
                fLoader.LoadBilKomp();
            }
            catch (Exception j) { if (j.Source != null) await fLoader.SaveBilKomp(); missingFile += "\n" + j.Message; }
            try
            {
                fLoader.LoadBussKomp();
            }
            catch (Exception k) { if (k.Source != null) await fLoader.SaveBussKomp(); missingFile += "\n" + k.Message; }
            try
            {
                fLoader.LoadMcKomp();
            }
            catch (Exception l) { if (l.Source != null) await fLoader.SaveMcKomp(); missingFile += "\n" + l.Message; }
        }
    }
}
