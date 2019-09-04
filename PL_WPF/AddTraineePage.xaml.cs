﻿using System;
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
    /// Interaction logic for AddTrainees.xaml
    /// </summary>
    public partial class AddTraineePage: Page
    {
        
        private Trainee Trainee;
        private IBL BL;
        #region Ctor
        public AddTraineePage()
        {
            Trainee = new Trainee();
            BL = FactoryBL.getInstance(); // give an instance of IBL

            InitializeComponent(); // run constructor for all elements in the window, wout this a run time error occurs

            this.DataContext = this.Trainee; // bind Trainee to the data context, wthout this data context would be null and we wouldnt be able to bind at all

            // for each combobox give it the elements to display
            this.GenderComboBox.ItemsSource = Enum.GetValues(typeof(BE.Gender)); 
            this.GearBoxComboBox.ItemsSource = Enum.GetValues(typeof(BE.GearBox));
            this.VehicleTypeComboBox.ItemsSource = Enum.GetValues(typeof(BE.VehicleType));
            
        }
        #endregion

        #region Buttons
        /// <summary>
        /// Attemps to add the trainee to the DMV system, through checking for valid input,
        /// and creates an error message for invalid input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddTrainee_Button(object sender, RoutedEventArgs e)
        {
            // ensure the Trainee is valid, if not, display error message
            if (!ValidTrainee())
            {
                MessageBox.Show("Not all of the inputs were correct.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // attempt to add to BL, if add fails, show error message
            if (!BL.addTrainee(Trainee))
            {
                MessageBox.Show("Not all of the inputs were correct.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //go to Home Page
            Home home = new Home();
            this.NavigationService.Navigate(home);
        }

        /// <summary>
        /// cancel and close the page, losing all progress on the page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Cancel_Button(object sender, RoutedEventArgs e)
        {
            // confrim user's desire to close, and if so, go to home page
            MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel?", "Confirm Cancelation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Home home = new Home();
                this.NavigationService.Navigate(home);
            }
        }
        #endregion

        #region Utility Functions
        /// <summary>
        /// Checks if entered information into the page is vald
        /// </summary>
        /// <returns></returns>
        private bool ValidTrainee()
        {
            bool flag = true;
            // check for valid information, if any information is invalid, write to console, and set the flag to false.
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
        #endregion
    }
}