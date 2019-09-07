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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BE;
using BL;

namespace PL_WPF
{
    /// <summary>
    /// Interaction logic for EditTesterPage.xaml
    /// </summary>
    public partial class EditTesterPage : Page
    {
        // data members
        Tester Tester;
        IBL BL;
        #region Constructors
        public EditTesterPage()
        {
            Tester = new Tester();
            BL = FactoryBL.getInstance(); // give an instance of IBL
            InitializeComponent(); // run constructor for all elements in the window

            this.DataContext = this.Tester; // bind Tester to the data context
            //combo box options
            this.GenderComboBox.ItemsSource = Enum.GetValues(typeof(BE.Gender));
            this.VehicleTypeComboBox.ItemsSource = Enum.GetValues(typeof(BE.VehicleType));
        }
        public EditTesterPage(String ID)
        {
            BL = FactoryBL.getInstance();
            // get all testers with the passed in ID
            Tester = BL.getAllTesters(new Func<Tester, bool>(t => t.ID == ID)).FirstOrDefault();
            InitializeComponent();

            this.DataContext = this.Tester;
            this.GenderComboBox.ItemsSource = Enum.GetValues(typeof(BE.Gender));
            this.VehicleTypeComboBox.ItemsSource = Enum.GetValues(typeof(BE.VehicleType));
        }
        #endregion

        #region Buttons
        /// <summary>
        /// Attemp to submit the edited tester
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // if the tester is valid then update him and go to the home page, otherwise show error message
            if (ValidTester())
            {
                BL.updateTester(Tester);
                HomePage HomePage = new HomePage();
                this.NavigationService.Navigate(HomePage);
            }
            else
            {
                MessageBox.Show("Something went wrong", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// cancel and lose all progress on the page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // give the user an option to not cancel. If he still wants to cancel then go to the home pagve
            MessageBoxResult result = MessageBox.Show("Are you sure you would like to cancel?", "Confirm Cancelation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                HomePage HomePage = new HomePage();
                this.NavigationService.Navigate(HomePage);
            }
        }
        #endregion
        #region Utility Functions
        /// <summary>
        ///  helper functions inorder to check for valid testers
        /// </summary>
        /// <returns></returns>
        private bool ValidTester()
        {
            String ErrorMessage = "";
            bool flag = true;

            if (!Person.validID(IDTextBox.Text))
            {
                flag = false;
                ErrorMessage += "ID Wrong";
            }
            if (DOBPicker.SelectedDate == new DateTime())
            {
                flag = false;
                ErrorMessage += "\nDate of Birth Wrong";
            }
            if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text))
            {
                flag = false;
                ErrorMessage += "\nFirst Name Wrong";
            }
            if (string.IsNullOrWhiteSpace(LastNameTextBox.Text))
            {
                flag = false;
                ErrorMessage += "\nLast Name Wrong";
            }
            if (string.IsNullOrWhiteSpace(StreetTextBox.Text))
            {
                flag = false;
                ErrorMessage += "\nStreet Name Wrong";
            }
            if (NumberIntUpDown.Value == null)
            {
                flag = false;
                ErrorMessage += "\nAddress Number Wrong";
            }
            if (string.IsNullOrWhiteSpace(CityTextBox.Text))
            {
                flag = false;
                ErrorMessage += "\nCity Wrong";
            }
            if (StartYearDatePicker.SelectedDate == new DateTime())
            {
                flag = false;
                ErrorMessage += "\nStart Date is Wrong";
            }
            try
            {
                new MailAddress(EmailTextBox.Text);
            }
            catch
            {
                flag = false;
                ErrorMessage += "\nEmail Wrong";
            }
            // output the error message to the user, informing him of all of the errors that occured
            Console.WriteLine(ErrorMessage);
            return flag;
        }
        #endregion
    }
}

