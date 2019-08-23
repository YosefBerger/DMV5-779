using BE;
using BL;
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
    /// Interaction logic for AddTrainee.xaml
    /// </summary>
    public partial class AddTrainee : Window
    {
        private Trainee Trainee;
        private IBL BL;
        public AddTrainee()
        {
            Trainee = new Trainee();
            BL = FactoryBL.getInstance();

            InitializeComponent();

            this.DataContext = this.Trainee;

            this.GenderComboBox.ItemsSource = Enum.GetValues(typeof(BE.Gender));
            //this.GearBoxComboBox.SelectedItem = BE.Gender.MALE;
            this.GearBoxComboBox.ItemsSource = Enum.GetValues(typeof(BE.GearBox));
            //this.GearBoxComboBox.SelectedItem = BE.GearBox.AUTOMATIC;
            this.VehicleTypeComboBox.ItemsSource = Enum.GetValues(typeof(BE.VehicleType));
            //this.VehicleTypeComboBox.SelectedItem = BE.VehicleType.PRIVATE;
        }

        public void AddTrainee_Button(object sender, RoutedEventArgs e)
        {
            if (!ValidTrainee())
            {
                MessageBox.Show("Not all of the inputs were correct.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            if (!BL.addTrainee(Trainee))
            {
                MessageBox.Show("Not all of the inputs were correct.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            Trainee added = BL.getAllTrainees(new Func<Trainee, bool>( t => t.ID == this.Trainee.ID)).FirstOrDefault();
            MessageBox.Show(Trainee.ToString() + "\n----------\n" + added.ToString());
            
            this.Close();
        }

        public void Cancel_Button(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel?", "Confirm Cancelation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private bool ValidTrainee()
        {
            return true;
        }
    }
}
