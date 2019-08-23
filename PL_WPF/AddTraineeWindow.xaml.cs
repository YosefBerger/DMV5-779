using BE;
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
        public BE.Trainee Trainee { get; set; }

        BL.IBL bl;
        public AddTraineeWindow()
        {
            InitializeComponent();
            Trainee = new BE.Trainee();
            this.DataContext = this;
            bl = BL.FactoryBL.getInstance();
            
            
            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(BE.Gender));
            //this.gearBoxComboBox.
            this.gearBoxComboBox.ItemsSource = Enum.GetValues(typeof(BE.GearBox));
            this.vehicleTypeComboBox.ItemsSource = Enum.GetValues(typeof(BE.VehicleType));
            //this.genderComboBox.SelectedItem = BE.Gender.FEMALE;
        }

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        

        private void AddTraineeButton_Click(object sender, RoutedEventArgs e)
        {
            if (true)
            {
                MessageBox.Show("Some of the information was invalid.\nPlease try again",
                                "Submission Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            

            Console.WriteLine(Trainee);
            bl.addTrainee(Trainee);

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
