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
namespace GUI.Login
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private const string _errorMsg = "Inloggningen misslyckades";
        InterfaceLoadSave IUser = new UserSamling();
        UserSamling userSamling = new UserSamling();
        public string filePath { get; set; } = "C:/Users/pc/Documents/GitHub/jonasmats/Projektuppgift/Logic/DAL/User.json";
        


        private LoginService _loginService;
        public LoginPage()
        {
            InitializeComponent();

            try
            {
                userSamling.users = IUser.Load<User>(filePath);
            }
            catch
            {
                System.Windows.MessageBox.Show("Ladda User json-filen yo");
            }
            _loginService = new LoginService();

            _loginService.filePathGet = filePath;
            tbUsernam.Text = "Bosse";
            pbPassword.Password = "Meckarn123";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = tbUsernam.Text;
            string password = pbPassword.Password;

            bool successful = _loginService.Login(username, password);

            if (successful)
            {

                HomePage homePage = new HomePage();

                this.NavigationService.Navigate(homePage);
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
            FileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            filePath = fileDialog.FileName;
            userSamling.users = IUser.Load<User>(filePath);
            _loginService.filePathGet = filePath;
        }
            
        
    }
}
