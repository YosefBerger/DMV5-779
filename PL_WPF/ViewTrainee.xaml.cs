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
    /// Interaction logic for ViewTrainee.xaml
    /// </summary>
    public partial class ViewTrainee : Window
    {
        Trainee Trainee;
        IBL BL;
        public ViewTrainee()
        {
            InitializeComponent();
            this.DataContext = this.Trainee;
            DOBTextBlock.Text = Trainee.BirthDay.ToString("mm/dd/yyy");
        }
        public ViewTrainee(Trainee trainee)
        {
            InitializeComponent();

            BL = FactoryBL.getInstance();
            Trainee = trainee.Clone();
            DOBTextBlock.Text = Trainee.BirthDay.ToString("mm/dd/yyy");
            this.DataContext = this.Trainee;
        }
        public ViewTrainee(String ID)
        {
            InitializeComponent();

            BL = FactoryBL.getInstance();
            Trainee = BL.getAllTrainees(new Func<Trainee, bool> ( t => t.ID == ID )).FirstOrDefault();
            DOBTextBlock.Text = Trainee.BirthDay.ToString("mm/dd/yyy");
            this.DataContext = this.Trainee;
        }

        private void EditTrainee_Button(object sender, RoutedEventArgs e)
        {
            EditTrainee editTrainee = new EditTrainee(Trainee.ID)
            {
                Owner = this
            };
            editTrainee.Show();
        }

        private void Delete_Button(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you would like to delete the information for " + Trainee.FirstName + " " + Trainee.LastName + "?\nThis action cannot be undone!",
                                                      "Delete Trainee", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                if (!BL.removeTrainee(Trainee))
                {
                    MessageBox.Show("An error removing occured", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                this.Close();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
