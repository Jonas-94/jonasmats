using Logic.DAL;
using Logic.Entities;
using System.Windows;
using System.Windows.Controls;
namespace GUI.Home
{
    /// <summary>
    /// Interaction logic for SkapaUserPage.xaml
    /// </summary>
    public partial class SkapaUserPage : Page
    {
        FileLoader fLoader = new FileLoader();
        public SkapaUserPage()
        {
            InitializeComponent();
            try { fLoader.LoadMekaniker(); } catch { fLoader.SaveMekaniker(); }
            try { fLoader.LoadUsers(); } catch { fLoader.SaveUser(); }
            MekGrid.ItemsSource = fLoader.mekSamling.mekaniker;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnSkapaUser_Click(object sender, RoutedEventArgs e)
        {
            int IDnr = 0;
            Mekaniker m;
            bool run = true;
            string username = txtUsername.Text + "@meckarna.com";
            string password = txtPassword.Text;
            while (run)
            {

                if (MekGrid.SelectedItem is Mekaniker)
                {
                    m = (Mekaniker)MekGrid.SelectedItem;
                    IDnr = m.Id;
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Välj en mekaniker!");
                    break;
                }
                if (fLoader.userSamling.users.Exists(x => x.Username.Equals(username)))
                {
                    System.Windows.Forms.MessageBox.Show("Användarnamnet är taget!");
                    break;
                }
                else if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    System.Windows.Forms.MessageBox.Show("Fyll i Användarnamn  och lösenord!");
                    break;
                }
                else if (fLoader.userSamling.users.Exists(x => x.ID.Equals(IDnr)))
                {
                    System.Windows.Forms.MessageBox.Show("Användaren är redan registrerad!");
                    break;
                }
                User user = new User { Username = username, Password = password, ID = IDnr };
                fLoader.userSamling.users.Add(user);
                fLoader.SaveUser();
                run = false;
                System.Windows.Forms.MessageBox.Show($"Användaren:    {username}    skapades!");
            }
        }
    }
}

