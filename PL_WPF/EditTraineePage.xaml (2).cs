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
        #region Constructors
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
        #endregion

        #region Buttons
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

        /// <summary>
        /// cancel and close the page, losing all progress on the page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Cancel_Button(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel?", "Confirm Cancelation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                HomePage HomePage = new HomePage();
                this.NavigationService.Navigate(HomePage);
            }
        }
        #endregion

        #region Utility Functions
        /// <summary>
        /// Checks if entered information into the page is valid
        /// </summary>
        /// <returns></returns>
        private bool ValidTrainee()
        {
            String errorText = "";
            bool flag = true;
            // check for valid information, if any information is invalid, write to console, and set the flag to false.
            if (!Person.validID(IDTextBox.Text))
            {
                flag = false;
                errorText += "ID Wrong";
            }
            if (DOBPicker.SelectedDate == new DateTime())
            {
                flag = false;
                errorText += "\nDate of Birth Wrong";
            }
            if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text))
            {
                flag = false;
                errorText += "\nFirst Name Wrong";
            }
            if (string.IsNullOrWhiteSpace(LastNameTextBox.Text))
            {
                flag = false;
                errorText += "\nLast Name Wrong";
            }
            if (string.IsNullOrWhiteSpace(StreetTextBox.Text))
            {
                flag = false;
                errorText += "\nStreet Name Wrong";
            }
            if (NumberIntUpDown.Value == null)
            {
                flag = false;
                errorText += "\nAddress Number Wrong";
            }
            if (string.IsNullOrWhiteSpace(CityTextBox.Text))
            {
                flag = false;
                errorText += "\nCity Wrong";
            }
            if (string.IsNullOrWhiteSpace(DrivingSchoolNameTextBox.Text))
            {
                flag = false;
                errorText += "\nSchool Name Wrong";
            }
            if (string.IsNullOrWhiteSpace(DrivingInstructorNameTextBox.Text))
            {
                flag = false;
                errorText += "\nInstructor Name Wrong";
            }
            try
            {
                new MailAddress(EmailTextBox.Text);
            }
            catch
            {
                flag = false;
                errorText += "\nEmal Wrong";
            }
            Console.WriteLine(errorText);
            return flag;
        }
        #endregion
    }
}
