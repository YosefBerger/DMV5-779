using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL_WPF
{
    /// <summary>
    /// Interaction logic for AddTrainee.xaml
    /// </summary>
    public partial class AddTrainee : Window
    {
        private Trainee Trainee;
        private IBL BL;
        public AddTrainee()
        {
            Trainee = new Trainee();
            BL = FactoryBL.getInstance();

            InitializeComponent();

            this.DataContext = this.Trainee;

            this.GenderComboBox.ItemsSource = Enum.GetValues(typeof(BE.Gender));
            //this.GearBoxComboBox.SelectedItem = BE.Gender.MALE;
            this.GearBoxComboBox.ItemsSource = Enum.GetValues(typeof(BE.GearBox));
            //this.GearBoxComboBox.SelectedItem = BE.GearBox.AUTOMATIC;
            this.VehicleTypeComboBox.ItemsSource = Enum.GetValues(typeof(BE.VehicleType));
            //this.VehicleTypeComboBox.SelectedItem = BE.VehicleType.PRIVATE;
        }

        public void AddTrainee_Button(object sender, RoutedEventArgs e)
        {
            if (!ValidTrainee())
            {
                MessageBox.Show("Not all of the inputs were correct.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            if (!BL.addTrainee(Trainee))
            {
                MessageBox.Show("Not all of the inputs were correct.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Trainee added = BL.getAllTrainees(new Func<Trainee, bool>( t => t.ID == this.Trainee.ID)).FirstOrDefault();
            MessageBox.Show(Trainee.ToString() + "\n----------\n" + added.ToString());
            
            this.Close();
        }

        public void Cancel_Button(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel?", "Confirm Cancelation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
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
            if(DOBPicker.SelectedDate == new DateTime())
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
            //flag = flag && Person.validID(IDTextBox.Text);
            //flag = flag && (DOBPicker.SelectedDate != new DateTime());
            //flag = flag && !string.IsNullOrWhiteSpace(FirstNameTextBox.Text);
            //flag = flag && !string.IsNullOrWhiteSpace(LastNameTextBox.Text);
            //flag = flag && NumberIntUpDown.Value != null;
            //flag = flag && !string.IsNullOrWhiteSpace(StreetTextBox.Text);
            //flag = flag && !string.IsNullOrWhiteSpace(CityTextBox.Text);
            //flag = flag && !string.IsNullOrWhiteSpace(DrivingSchoolNameTextBox.Text);
            //flag = flag && !string.IsNullOrWhiteSpace(DrivingInstructorNameTextBox.Text);
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
