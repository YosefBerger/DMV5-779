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
    /// Interaction logic for EditTester.xaml
    /// </summary>
    public partial class EditTester : Window
    {
        Tester Tester;
        IBL BL;
        public EditTester()
        {
            Tester = new Tester();
            BL = FactoryBL.getInstance();
            InitializeComponent();

            this.DataContext = this.Tester;
            this.GenderComboBox.ItemsSource = Enum.GetValues(typeof(BE.Gender));
            this.VehicleTypeComboBox.ItemsSource = Enum.GetValues(typeof(BE.VehicleType));
        }
        public EditTester(String ID)
        {
            BL = FactoryBL.getInstance();
            Tester = BL.getAllTesters(new Func<Tester, bool>(t => t.ID == ID)).FirstOrDefault();
            InitializeComponent();

            this.DataContext = this.Tester;
            this.GenderComboBox.ItemsSource = Enum.GetValues(typeof(BE.Gender));
            this.VehicleTypeComboBox.ItemsSource = Enum.GetValues(typeof(BE.VehicleType));
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidTester())
            {
                BL.updateTester(Tester);
                this.Close();
            }
            else
            {
                MessageBox.Show("Something went wrong", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you would like to cancel?", "Confirm Cancelation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private bool ValidTester()
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
                Console.WriteLine("First Name wrong");
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
            if (StartYearDatePicker.SelectedDate == new DateTime())
            {
                flag = false;
                Console.WriteLine("Start date is wrong");
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
