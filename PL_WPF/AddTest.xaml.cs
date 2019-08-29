using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using BL;
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
    /// Interaction logic for AddTest.xaml
    /// </summary>
    public partial class AddTest : Window
    {
        DateTime TodayDate { get; set; }
        Test Test;
        IBL BL;
        public AddTest()
        {
            BL = FactoryBL.getInstance();
            TodayDate = DateTime.Today;
            Test = new Test();
            InitializeComponent();
            this.DataContext = Test;
            DatePicker.SelectedDate = DateTime.Today;
            HourPicker.Value = 9;
        }
        public AddTest(String PassedTraineeID)
        {
            BL = FactoryBL.getInstance();
            TodayDate = DateTime.Today;
            Test = new Test();
            InitializeComponent();
            this.DataContext = Test;
            DatePicker.SelectedDate = DateTime.Today;
            HourPicker.Value = 9;
        }

        private void TraineeListButton_Click(object sender, RoutedEventArgs e)
        {
            SelectTrainee selectTrainee = new SelectTrainee()
            {
                Owner = this
            };
            selectTrainee.ShowDialog();
        }

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

        private void HourPicker_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (HourPicker.Value == null || HourPicker.Value < 9 || HourPicker.Value > 15)
            {
                HourPicker.Value = 9;
            }
            Test.DateTime = Test.DateTime.Date.AddHours((double)HourPicker.Value);
            DateString.Text = Test.DateTime.ToString("MM/dd/yyyy HH:mm");
        }

        private void FindTesterButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Person.validID(TraineeIDTextBox.Text))
            {
                MessageBox.Show("Invalid ID");
                return;
            }
            if (string.IsNullOrWhiteSpace(Test.StartAddress.City) || string.IsNullOrWhiteSpace(Test.StartAddress.Street))
            {
                MessageBox.Show("Invalid Adress");
                return;
            }
            Trainee tmp = BL.getAllTrainees(new Func<Trainee, bool>(t => t.ID == TraineeIDTextBox.Text)).FirstOrDefault();
            if (((DateTime.Today - tmp.BirthDay).Days / 365) < Configuration.TRAINEE_MIN_AGE || tmp.NumDrivingLessons < Configuration.TRAINEE_MIN_LESSONS)
            {
                MessageBox.Show("Inelidgable Trainee");
                return;
            }

            SelectTester selectTester = new SelectTester(Test)
            {
                Owner = this
            };
            selectTester.ShowDialog();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you would like to cancel?", "Confrm Cancelation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        bool TestIsValid(Test test)
        {
            // Make sure the trainee exists
            Trainee trainee = BL.GetTraineeByID(test.TraineeId);
            if (trainee == null)
            {
                Console.WriteLine("Invalid TraineeID");
                return false;
            }

            // Make sure the Trainee hasn't already passed a test
            List<Test> testsTakenByTrainee = BL.traineeTests(trainee);
            List<Test> passedTests = (from t in testsTakenByTrainee
                                      where t.Result
                                      select t).ToList();
            if (passedTests.Count != 0)
            {
                Console.WriteLine("The Trainee has already passed a test");
                return false;
            }

            // Make sure that the Starting Address is valid
            if (test.StartAddress == null || test.StartAddress.City == null || test.StartAddress.Street == null || test.StartAddress.Number < 0)
            {
                Console.WriteLine("Invalid STartAddress");
                return false;
            }

            // Make sure there are no conflicting dates
            List<Test> conflictingTests = BL.getAllTests(new Func<Test, bool>
               (it => it.TraineeId == test.TraineeId && Math.Abs((test.DateTime.Date - it.DateTime.Date).Days) <= Configuration.DAYS_FROM_TEST));
            if (conflictingTests.Count != 0)
            {
                DateTime nonConfDate = BL.GetNonConflictingDate(test);
                Console.WriteLine("Conflicting date, try " + nonConfDate.ToString("mm/dd//yyyy"));
                return false;
            }

            return true;
        }
    }
}
