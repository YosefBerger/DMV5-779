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
using BE;
using BL;

namespace PL_WPF
{
    /// <summary>
    /// Interaction logic for EditTestPage.xaml
    /// </summary>
    public partial class EditTestPage : Page
    {
        // data members
        Test test;
        IBL BL;
        #region Constructor
        public EditTestPage(Test test)
        {
            InitializeComponent(); 
            this.test = test.Clone();
            this.DataContext = this.test;
            TestDate.Text = test.DateTime.ToString("mm/dd/yyyy"); // convert the date of the test to a string
            TestHour.Text = test.DateTime.Hour + ":00"; // set the correct hour of the test
            BL = FactoryBL.getInstance();
        }
        #endregion

        #region Buttons
        /// <summary>
        /// submit the new information for the test
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            // if the test is able to be updated, go to the home page
            if (BL.updateTest(test))
            {
                HomePage HomePage = new HomePage();
                this.NavigationService.Navigate(HomePage);
            }
            // if nvalit information exists, inform the user
            MessageBox.Show("An error occured", "Delete Test", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// cancel and lose all progress on the page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // ask the user if he wants to cancel, and if so, go to the home page
            MessageBoxResult result = MessageBox.Show("Are you sure you would like to cancel?", "Confirm Cancelation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes)
            {
                HomePage HomePage = new HomePage();
                this.NavigationService.Navigate(HomePage);
            }
            
        }
        #endregion
    }
}
