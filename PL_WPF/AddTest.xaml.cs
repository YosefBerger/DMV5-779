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
        public AddTest()
        {
            this.DataContext = Test;
            TodayDate = DateTime.Today;
            Test = new Test();
            InitializeComponent();
            DatePicker.SelectedDate = DateTime.Today;
            HourPicker.Value = 9;
        }
        public AddTest(String PassedTraineeID)
        {
            this.DataContext = Test;
            TodayDate = DateTime.Today;
            Test = new Test();
            InitializeComponent();
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

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you would like to cancel?", "Confrm Cancelation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}
