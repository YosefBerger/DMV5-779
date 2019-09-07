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
    /// Interaction logic for AddTestPage.xaml
    /// </summary>
    public partial class AddTestPage : Page
    {
        /// <summary>
        /// data members
        /// </summary>
        DateTime TodayDate { get; set; }
        Test Test;
        IBL BL;
        #region Constructors
        public AddTestPage()
        {
            BL = FactoryBL.getInstance();
            TodayDate = DateTime.Today;
            Test = new Test();
            InitializeComponent();
            this.DataContext = Test;
            DatePicker.SelectedDate = DateTime.Today;
            HourPicker.Value = 9;
        }
        public AddTestPage(String PassedTraineeID)
        {
            BL = FactoryBL.getInstance();
            TodayDate = DateTime.Today;
            Test = new Test();
            InitializeComponent();
            this.DataContext = Test;
            DatePicker.SelectedDate = DateTime.Today;
            HourPicker.Value = 9;
        }
        #endregion

        #region Buttons
        /// <summary>
        /// the user types into the textbox a trainee ID that he is searching for
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TraineeListButton_Click(object sender, RoutedEventArgs e)
        {
            SelectTrainee selectTrainee = new SelectTrainee();
            selectTrainee.ShowDialog();
            TraineeIDTextBox.Text = selectTrainee.SelectedID;
        }
        /// <summary>
        /// Search for a tester based on the filled in information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FindTesterButton_Click(object sender, RoutedEventArgs e)
        {
            // if the ID is invalid, display error message
            if (!Person.validID(TraineeIDTextBox.Text))
            {

                MessageBox.Show("Invalid ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // if address is invalid or empty, display error message
            if (string.IsNullOrWhiteSpace(Test.StartAddress.City) || string.IsNullOrWhiteSpace(Test.StartAddress.Street))
            {
                MessageBox.Show("Invalid Address", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // check that the trainee is old enough and that he has done enough lessons.  If not, display error message
            Trainee tmp = BL.getAllTrainees(new Func<Trainee, bool>(t => t.ID == TraineeIDTextBox.Text)).FirstOrDefault();
            if (((DateTime.Today - tmp.BirthDay).Days / 365) < Configuration.TRAINEE_MIN_AGE || tmp.NumDrivingLessons < Configuration.TRAINEE_MIN_LESSONS)
            {
                MessageBox.Show("Ineligible Trainee", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            SelectTester selectTester = new SelectTester(Test);

            selectTester.ShowDialog();
            // If the tester is cacneled, then go to home page
            if (!selectTester.IsCanceled)
            {
                HomePage HomePage = new HomePage();
                this.NavigationService.Navigate(HomePage);
            }
        }

        /// <summary>
        /// cancel, and all current progress is lost
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // ask user if he is sure about canceling, and if yes, then go to home page.  If not, then stay on the current page
            MessageBoxResult result = MessageBox.Show("Are you sure you would like to cancel?", "Confrm Cancelation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                HomePage HomePage = new HomePage();
                this.NavigationService.Navigate(HomePage);

            }
        }
        #endregion 

        
        /// <summary>
        /// choosing correct date for the test
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatePicker_LostFocus(object sender, RoutedEventArgs e)
        {
            
            if (((DatePicker)sender).SelectedDate.Value.CompareTo(DateTime.Today) < 0 || ((int)((DatePicker)sender).SelectedDate.Value.DayOfWeek) > 4)
            {
                ((DatePicker)sender).SelectedDate = Test.DateTime;
            }
            else
            {
                Test.DateTime = ((DatePicker)sender).SelectedDate.Value.Date.AddHours((double)HourPicker.Value);
                DateString.Text = Test.DateTime.ToString("MM/dd/yyyy HH:mm");
            }
        }

        /// <summary>
        /// choosing the hour for the test
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HourPicker_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            // if the hour is invalid or not chosen, then it will be defaulted to 9
            if (HourPicker.Value == null || HourPicker.Value < 9 || HourPicker.Value > 15)
            {
                HourPicker.Value = 9;
            }
            Test.DateTime = Test.DateTime.Date.AddHours((double)HourPicker.Value);
            DateString.Text = Test.DateTime.ToString("MM/dd/yyyy HH:mm");
        }


        #region Utility Functions
        /// <summary>
        /// Check that the filled out test is valid
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>

        bool TestIsValid(Test test)
        {
            bool tmp = true;
            String errorText = "";
            // Make sure the trainee exists
            Trainee trainee = BL.GetTraineeByID(test.TraineeId);
            if (trainee == null)
            {
                errorText += "Trainee not found ";
                tmp = false;
            }

            // Make sure the Trainee hasn't already passed a test
            List<Test> testsTakenByTrainee = BL.traineeTests(trainee);
            List<Test> passedTests = new List<Test>();
            foreach (Test t in testsTakenByTrainee)
            {
                if (t.Result)
                {
                    passedTests.Add(t);
                }
            }
            if (passedTests.Count != 0)
            {
                errorText  += "\nThe Trainee has already passed a test ";
                tmp = false;
            }

            // Make sure that the Starting Address is valid
            if (test.StartAddress == null || test.StartAddress.City == null || test.StartAddress.Street == null || test.StartAddress.Number < 0)
            {
                errorText += "\nInvalid Start Address ";
                tmp = false;
            }

            // Make sure there are no conflicting dates
            List<Test> conflictingTests = BL.getAllTests(new Func<Test, bool>
               (it => it.TraineeId == test.TraineeId && Math.Abs((test.DateTime.Date - it.DateTime.Date).Days) <= Configuration.DAYS_FROM_TEST));
            if (conflictingTests.Count != 0)
            {
                DateTime nonConfDate = BL.GetNonConflictingDate(test);
                errorText += "Conflicting date, try " + nonConfDate.ToString("mm/dd//yyyy");
                tmp = false;
            }

            Console.WriteLine(errorText);
            return tmp;
        }
        #endregion
    }
}
