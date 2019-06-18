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
using System.Windows.Shapes;

namespace PL_WPF
{
    /// <summary>
    /// Interaction logic for AddTrainneWindow.xaml
    /// </summary>
    public partial class AddTrainneWindow : Window
    {
        public AddTrainneWindow()
        {
            InitializeComponent();
            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(BE.Gender));
            this.gearBoxComboBox.ItemsSource = Enum.GetValues(typeof(BE.GearBox));
            this.vehicleTypeComboBox.ItemsSource = Enum.GetValues(typeof(BE.VehicleType));
            BE.Trainee trainee = new BE.Trainee
            {
                ID = "1234",
                GearBox = BE.GearBox.MANUAL
            };
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            trainee = new BE.Trainee
            {
                ID = iDTextBox.Text,
                GearBox = (BE.GearBox)gearBoxComboBox.SelectedItem
            };
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource traineeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("traineeViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // traineeViewSource.Source = [generic data source]
        }
    }
}
