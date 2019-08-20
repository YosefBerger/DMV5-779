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
    /// Interaction logic for AddTraineeWindow.xaml
    /// </summary>
    public partial class AddTraineeWindow : Window
    {
        BE.Trainee trainee;
        BL.IBL bl;
        public AddTraineeWindow()
        {
            InitializeComponent();

            bl = BL.FactoryBL.getInstance();

            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(BE.Gender));
            this.gearBoxComboBox.ItemsSource = Enum.GetValues(typeof(BE.GearBox));
            this.vehicleTypeComboBox.ItemsSource = Enum.GetValues(typeof(BE.VehicleType));
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            this.Owner.Hide();
        }
        private new void Closed(object sender, RoutedEventArgs e)
        {
            this.Owner.Show();
        }

        private void AddTraineeButton_Click(object sender, RoutedEventArgs e)
        {
            if (!validteAllFields())
            {
                MessageBox.Show("Some of the information was invalid.\nPlease try again",
                                "Submission Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            trainee = new BE.Trainee
            {
                ID = IDTextBox.Text,
                GearBox = (BE.GearBox)gearBoxComboBox.SelectedItem,
                VehicleType = (BE.VehicleType)vehicleTypeComboBox.SelectedItem,
                Gender = (BE.Gender)genderComboBox.SelectedItem,
                FirstName = FirstNameTextBox.Text,
                LastName = LastNameTextBox.Text,
                BirthDay = DateTime.Parse(DOBDatePicker.Text),
                DrivingSchool = DrivingSchoolTextBox.Text,
                InstructorName = DrivingInstructorTextBox.Text,
                NumDrivingLessons = Convert.ToInt32(NumberOfLessonsTetBox.Text),
                Address = new BE.Address
                {
                    Number = Convert.ToInt32(AddressNumberTextBox.Text),
                    Street = AddressStreetTextBox.Text,
                    City = AddressCityTextBlock.Text
                }
            };

            bl.addTrainee(trainee);

            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you would like to cancel?", "Cancel", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource traineeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("traineeViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // traineeViewSource.Source = [generic data source]
        }

        private bool validteAllFields()
        {
            bool flag = true;
            BE.ValidateEventArgs e = new BE.ValidateEventArgs();

            // Validation code
            // Is ID present and valid
            ValidateID(IDTextBox, e);
            flag = flag && e.isValid;

            ValidateEmail(EmailTextBlock, e);
            flag = flag && e.isValid;

            //Is FN present
            ValidateString(FirstNameTextBox, e);
            flag = flag && e.isValid;

            //Is LN resent
            ValidateString(LastNameTextBox, e);
            

            //Is address infor present and is number a number
            ValidateNumber(AddressNumberTextBox, e);
            flag = flag && e.isValid;

            ValidateString(AddressStreetTextBox, e);
            flag = flag && e.isValid;

            ValidateString(AddressCityTextBlock, e);
            flag = flag && e.isValid;


            //Is driving school present
            ValidateString(DrivingSchoolTextBox, e);
            flag = flag && e.isValid;

            //Is instructor present
            ValidateString(DrivingInstructorTextBox, e);

            //Is a date selected
            ValidateDOB(DOBDatePicker, e);
            flag = flag && e.isValid;

            //Is num lessons present and is it a number?
            ValidateNumber(NumberOfLessonsTetBox, e);
            flag = flag && e.isValid;

            return flag;
        }

        private void ValidateID(object sender, EventArgs e)
        {
            bool tmp = true;
            String id = ((TextBox)sender).Text;
            if (id == null || id.Length != 9 || !BE.Person.validID(id))
            {
                ((TextBox)sender).Background = Brushes.LightPink;
                tmp = false;
            }
            else
            {
                ((TextBox)sender).Background = Brushes.White;
            }

            if (e is BE.ValidateEventArgs)
            {
                ((BE.ValidateEventArgs)e).isValid = tmp;
            }
        }

        private void ValidateString(object sender, EventArgs e)
        {
            bool tmp = true;
            String str = ((TextBox)sender).Text;
            if (str == null || str.CompareTo("") == 0)
            {
                ((TextBox)sender).Background = Brushes.LightPink;
                tmp = false;
            }
            else
            {
                ((TextBox)sender).Background = Brushes.White;
            }

            if (e is BE.ValidateEventArgs)
            {
                ((BE.ValidateEventArgs)e).isValid = tmp;
            }
        }

        private void ValidateNumber(object sender, EventArgs e)
        {
            bool tmp = true;
            String str = ((TextBox)sender).Text;
            int tmpint = -1;

            ((TextBox)sender).Background = Brushes.White;
            if (str == null || str.CompareTo("") == 0)
            {
                ((TextBox)sender).Background = Brushes.LightPink;
                tmp = false;
            }
            try
            {
                Int32.TryParse(str, out tmpint);
            }
            catch
            {
                ((TextBox)sender).Background = Brushes.LightPink;
                tmp = false;
            }
            if (tmpint < 0)
            {
                ((TextBox)sender).Background = Brushes.LightPink;
                tmp = false;
            }

            if (e is BE.ValidateEventArgs)
            {
                ((BE.ValidateEventArgs)e).isValid = tmp;
            }
        }

        private void ValidateEmail(object sender, EventArgs e)
        {
            bool tmp = true;
            String str = ((TextBox)sender).Text;
            ((TextBox)sender).Background = Brushes.White;

            try
            {
                MailAddress email = new MailAddress(str);
            }
            catch
            {
                ((TextBox)sender).Background = Brushes.LightPink;
                tmp = false;
            }

            if (e is BE.ValidateEventArgs)
            {
                ((BE.ValidateEventArgs)e).isValid = tmp;
            }
        }

        private void ValidateDOB(object sender, EventArgs e)
        {
            bool tmp = true;
            if (((DatePicker)sender).DisplayDate == null)
            {
                ((DatePicker)sender).Background = Brushes.LightPink;
                tmp = false;
            }
            else
            {
                ((DatePicker)sender).Background = Brushes.LightPink;
            }

            if (e is BE.ValidateEventArgs)
            {
                ((BE.ValidateEventArgs)e).isValid = tmp;
            }
        }
    }
}
