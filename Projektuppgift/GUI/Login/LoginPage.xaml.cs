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
        InterfaceLoadSave IUser = new UserSamling();
        UserSamling userSamling = new UserSamling();
        Regex reg = new Regex();
        public string filePath { get; set; }// = "C:/Users/pc/Documents/GitHub/jonasmats/Projektuppgift/Logic/DAL/User.json";
        
        int id { get; set; }

        private LoginService _loginService;
        public LoginPage()
        {
            
            InitializeComponent();
            
            
            _loginService = new LoginService();
            try
            {
                userSamling.users = IUser.Load<User>(filePath);
                _loginService.filePathGet = filePath;
            }
            catch
            {
                System.Windows.MessageBox.Show("Kunde inte ladda User json-filen yo");
            }

            tbUsernam.Text = "Bosse";
            pbPassword.Password = "Meckarn123";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string username = tbUsernam.Text;
            string password = pbPassword.Password;
            bool regexSuccessful = reg.checkForEmail(username);
            bool successful = _loginService.Login(username, password);
            
            if (successful && username == "Bosse")
            {

                //HomePage homePage = new HomePage();
                //MekanikerPage mekPage = new MekanikerPage();
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

        private void btnfindUserFile_Click(object sender, RoutedEventArgs e)
        {
            FindFolderPath();
            
        }
        public void FindFolderPath()
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.ShowDialog();
            string filePath = folder.SelectedPath + "/User.json";
            fLoader.FindFolderPath(folder.SelectedPath);
            try
            {
                userSamling.users = IUser.Load<User>(filePath);
                _loginService.filePathGet = filePath;
            }
            catch
            {
                System.Windows.MessageBox.Show("Kunde inte ladda User json-filen yo");
            }
        }
    }
}
