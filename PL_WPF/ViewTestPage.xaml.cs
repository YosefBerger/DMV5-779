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
    /// Interaction logic for ViewTestPage.xaml
    /// </summary>
    public partial class ViewTestPage : Page
    {
        // data members
        Test test;
        IBL BL;

        #region Constructors
        public ViewTestPage()
        {
            InitializeComponent();
        }
        public ViewTestPage(Test test)
        {
            // Use the passed test
            this.test = test;
            InitializeComponent();
            this.DataContext = this.test;
            // Since we cannot bind a TextBlock to a DateTime, jsut get the strings
            TestDate.Text = test.DateTime.ToString("mm/dd/yyyy");
            TestHour.Text = test.DateTime.Hour + ":00";
            BL = FactoryBL.getInstance();
        }
        #endregion

        #region Buttons
        /// <summary>
        /// when this button is selected, the edit test page is opened for the current test
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // the test selected to edit is opened in the Edit Test Page
            EditTestPage edit = new EditTestPage(test);
            this.NavigationService.Navigate(edit);
        }

        /// <summary>
        /// delete test button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // display message box asking user if he is sure about deleting the test
            MessageBoxResult result = MessageBox.Show("Are you sure you would like to delete this test?\nThis cation cannot be undone.", "Confirm Cancelation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                bool tmp = BL.removeTest(test);

                // if the test was successfuly removed, the user is informed.  If not, the user is told an error occurred
                MessageBox.Show(tmp ? "The test was removed." : "An error occured", "Delete Test", MessageBoxButton.OK, tmp ? MessageBoxImage.Asterisk : MessageBoxImage.Error);

                // go to the home page 
                HomePage HomePage = new HomePage();
                this.NavigationService.Navigate(HomePage);
            }
        }
        #endregion
    }
}
