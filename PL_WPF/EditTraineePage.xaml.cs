using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BL;
using BE;
using System.Net.Mail;

namespace PL_WPF
{
    /// <summary>
    /// Interaction logic for EditTraineePage.xaml
    /// </summary>
    public partial class EditTraineePage : Page
    {
        private Trainee Trainee;
        private IBL BL;
        public EditTraineePage()
        {
            Trainee = new Trainee();
            BL = FactoryBL.getInstance();

            InitializeComponent();

            this.DataContext = this.Trainee;

            this.GenderComboBox.ItemsSource = Enum.GetValues(typeof(BE.Gender));
            this.GearBoxComboBox.ItemsSource = Enum.GetValues(typeof(BE.GearBox));
            this.VehicleTypeComboBox.ItemsSource = Enum.GetValues(typeof(BE.VehicleType));
        }
        public EditTraineePage(String ID)
        {
            BL = FactoryBL.getInstance();
            Trainee = BL.getAllTrainees(new Func<Trainee, bool>(t => t.ID == ID)).FirstOrDefault();

            InitializeComponent();

            this.DataContext = this.Trainee;

            this.GenderComboBox.ItemsSource = Enum.GetValues(typeof(BE.Gender));
            this.GearBoxComboBox.ItemsSource = Enum.GetValues(typeof(BE.GearBox));
            this.VehicleTypeComboBox.ItemsSource = Enum.GetValues(typeof(BE.VehicleType));
        }

        public void UpdateTrainee_Button(object sender, RoutedEventArgs e)
        {
            if (!ValidTrainee())
            {
                MessageBox.Show("Not all of the inputs were correct.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!BL.updateTrainee(Trainee))
            {
                MessageBox.Show("An error occured updating", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            HomePage HomePage = new HomePage();
            this.NavigationService.Navigate(HomePage);
        }

        public void Cancel_Button(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel?", "Confirm Cancelation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                HomePage HomePage = new HomePage();
                this.NavigationService.Navigate(HomePage);
            }
        }

        private bool ValidTrainee()
        {
            bool flag = true;
            if (!Person.validID(IDTextBox.Text))
            {
                flag = false;
                Console.WriteLine("ID wrong");
            }
            if (DOBPicker.SelectedDate == new DateTime())
            {
                flag = false;
                Console.WriteLine("DOB Wrong");
            }
            if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text))
            {
                flag = false;
                Console.WriteLine("First NAme wrong");
            }
            if (string.IsNullOrWhiteSpace(LastNameTextBox.Text))
            {
                flag = false;
                Console.WriteLine("Last Name wrong");
            }
            if (string.IsNullOrWhiteSpace(StreetTextBox.Text))
            {
                flag = false;
                Console.WriteLine("Street name wrong");
            }
            if (NumberIntUpDown.Value == null)
            {
                flag = false;
                Console.WriteLine("Address number wrong");
            }
            if (string.IsNullOrWhiteSpace(CityTextBox.Text))
            {
                flag = false;
                Console.WriteLine("City wrong");
            }
            if (string.IsNullOrWhiteSpace(DrivingSchoolNameTextBox.Text))
            {
                flag = false;
                Console.WriteLine("School name wrong");
            }
            if (string.IsNullOrWhiteSpace(DrivingInstructorNameTextBox.Text))
            {
                flag = false;
                Console.WriteLine("Instructor name wrong");
            }
            try
            {
                new MailAddress(EmailTextBox.Text);
            }
            catch
            {
                flag = false;
                Console.WriteLine("email wrong");
            }

            return flag;
        }
    }
}
