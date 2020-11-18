using Logic.DAL;
using Logic.Entities;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GUI.Home
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        FileLoader fLoader { get; set; } = new FileLoader();
        
        MethodHandler methods = new MethodHandler();
        public MekanikerSamling mekSamling { get; set; } = new MekanikerSamling();
        public FordonSamling fordonSamling { get; set; } = new FordonSamling();
        public ÄrendeSamling ärendeSamling { get; set; } = new ÄrendeSamling();
        public MotorcykelSamling motorcykelSamling { get; set; } = new MotorcykelSamling();
        public BilSamling bilSamling { get; set; } = new BilSamling();
        public BussSamling bussSamling { get; set; } = new BussSamling();
        public UserSamling userSamling { get; set; } = new UserSamling();
        public LastbilSamling lastbilSamling { get; set; } = new LastbilSamling();
        public List<Mekaniker> meksWithoutUser = new List<Mekaniker>();
        public Mekaniker ärendeMekaniker { get; set; }
        public Fordon ärendeFordon { get; set; }
        public int fordonindex { get; set; }
        public int mekint { get; set; }
        public int tab4ID { get; set; }
        public bool onoffÄrende { get; set; }

        public HomePage()
        {
            InitializeComponent();
            onoffÄrende = false;
            fLoader.home = this;
            methods.hp = this;
            try
            {
                fLoader.LoadFiles("Alla");
                dataGridFordon.ItemsSource = fordonSamling.fordon;
                tbÄrendeDataGrid.ItemsSource = ärendeSamling.ärenden;
                //dataGridMekaniker.ItemsSource = fLoader.mekSamling.mekaniker;
                tb3fordonDataGrid.ItemsSource = fordonSamling.fordon;
                tb3mekDataGrid1.ItemsSource = mekSamling.mekaniker;
            }
            catch
            {
                System.Windows.MessageBox.Show("Öppna DAL-mappen för att ladda alllllllt");
            }
            try
            {
                mekSamling = mekSamling;
                dataGridMekaniker.ItemsSource = mekSamling.mekaniker;
            }
            catch { }
        }
        //Knappen för att lägga till en ny Mekaniker
        private void BtnCreateMechanic_Click(object sender, RoutedEventArgs e)
        {
            methods.SkapaMekaniker();
            fLoader.SaveMekaniker();
        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        //Knapp som kan ladda DAL-mappen om den inte hittats
        private void laddaMekFil_Click(object sender, RoutedEventArgs e)
        {
            fLoader.FindFolderPath();
            try
            {
                fLoader.LoadFiles("Alla");
            }
            catch { System.Windows.MessageBox.Show("Kunde inte ladda mappen."); }
        }
        //metod som tar bort texten i TextBoxar och uncheckar Kompetens-bools i mekanikerfliken

        //metod som spara mekaniker till json
        private void saveMechFile_Click(object sender, RoutedEventArgs e)
        {
            fLoader.SaveMekaniker();
        }
        //Knapp som tar bort vald mekaniker
        private void btnDeleteMechanic_Click(object sender, RoutedEventArgs e)
        {
            methods.TaBortMekaniker();
            fLoader.SaveMekaniker();
        }
        private void CreateUser_Click_1(object sender, RoutedEventArgs e)
        {
            methods.SkapaUser();
            fLoader.SaveUser();
        }
        private void BtnCreateFordon_Click_2(object sender, RoutedEventArgs e)
        {
            methods.SkapaFordon();
            fLoader.SaveBilar();fLoader.SaveBussar();
            fLoader.SaveLastBilar();fLoader.SaveMotorcyklar();
            fLoader.FordonReload();
        }
        private void tb3btnMek_Click(object sender, RoutedEventArgs e)
        {
            methods.VisaFordonTillMekaniker();
        }
        MekanikerSamling ms { get; set; }
        private void tb3btnFordon_Click(object sender, RoutedEventArgs e)
        {
            methods.VisaMekanikerTillFordon();
        }
        private void tb3btnSkapaÄrende_Click(object sender, RoutedEventArgs e)
        {
            methods.SkapaÄrende();
            fLoader.SaveÄrenden();
        }

        private void tb4btnChooseUser_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < mekSamling.mekaniker.Count; i++)
            {
                if (tb4UserDataGrid.SelectedItem == mekSamling.mekaniker[i])
                {
                    tb4btnChooseUser.Background = Brushes.Green;
                    tab4ID = mekSamling.mekaniker[i].Id;
                }
            }
        }
        private void tb3cboxFordon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tb3cboxFordon.SelectedItem == "Alla fordon")
            {
                dataGridFordon.ItemsSource = null;
                dataGridFordon.ItemsSource = fordonSamling.fordon;
            }
            else if (tb3cboxFordon.SelectedItem == "Bilar")
            {
                dataGridFordon.ItemsSource = null;
                dataGridFordon.ItemsSource = bilSamling.Bilar;
            }
            else if (tb3cboxFordon.SelectedItem == "Bussar")
            {
                dataGridFordon.ItemsSource = null;
                dataGridFordon.ItemsSource = bussSamling.Bussar;
            }
            else if (tb3cboxFordon.SelectedItem=="Lastbilar")
            {
                dataGridFordon.ItemsSource = null;
                dataGridFordon.ItemsSource = lastbilSamling.lastbilar;
            }
            else if(tb3cboxFordon.SelectedItem == "Motorcyklar")
            {
                dataGridFordon.ItemsSource = null;
                dataGridFordon.ItemsSource = motorcykelSamling.motorcyklar;
            }
        }
        private void tb3visapåellerskapaÄrende_Click(object sender, RoutedEventArgs e)
        {
            methods.ÄrendePågåendeTillgängligSwitch();
        }

        private void btnAvslutaÄrende_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
            for (int i = 0; i < ärendeSamling.ärenden.Count; i++)
            { 
                if (tbÄrendeDataGrid.SelectedItem == ärendeSamling.ärenden[i])
                {
                    methods.HittaFordonIndex(ärendeSamling.ärenden[i].fordon, index, bilSamling.Bilar,
                        bussSamling.Bussar, lastbilSamling.lastbilar, motorcykelSamling.motorcyklar);
                    if (ärendeSamling.ärenden[i].fordon is Bil)
                        bilSamling.Bilar.RemoveAt(index);
                    else if (ärendeSamling.ärenden[i].fordon is Buss)
                        bussSamling.Bussar.RemoveAt(index);
                    else if (ärendeSamling.ärenden[i].fordon is Lastbil)
                        lastbilSamling.lastbilar.RemoveAt(index);
                    else if (ärendeSamling.ärenden[i].fordon is Motorcykel)
                        motorcykelSamling.motorcyklar.RemoveAt(index);

                    fLoader.SaveAllFordon();
                    fLoader.FordonReload();

                    ärendeSamling.ärenden[i].ÄrendeStatus = true;
                    foreach (var m in mekSamling.mekaniker)
                        if(ärendeSamling.ärenden[i].mekaniker == m)
                        {
                            m.Ärenden -= 1;
                            break;
                        }
                    fLoader.SaveMekaniker();
                    tbÄrendeDataGrid.ItemsSource = null;
                    tbÄrendeDataGrid.ItemsSource = ärendeSamling.ärenden;
                    fLoader.SaveMekaniker();
                } 
            }
        }
    }
}
