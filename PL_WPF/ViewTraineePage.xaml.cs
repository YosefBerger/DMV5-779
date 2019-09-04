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
using BL;
using BE;

namespace PL_WPF
{
    /// <summary>
    /// Interaction logic for ViewTraineePage.xaml
    /// </summary>
    public partial class ViewTraineePage : Page
    {
        Trainee Trainee;
        IBL BL;
        public ViewTraineePage()
        {
            InitializeComponent();
            this.DataContext = this.Trainee;
            DOBTextBlock.Text = Trainee.BirthDay.ToString("mm/dd/yyy");
        }
        public ViewTraineePage(Trainee trainee)
        {
            InitializeComponent();

            BL = FactoryBL.getInstance();
            Trainee = trainee.Clone();
            DOBTextBlock.Text = Trainee.BirthDay.ToString("mm/dd/yyy");
            this.DataContext = this.Trainee;
        }
        public ViewTraineePage(String ID)
        {
            InitializeComponent();

            BL = FactoryBL.getInstance();
            Trainee = BL.getAllTrainees(new Func<Trainee, bool>(t => t.ID == ID)).FirstOrDefault();
            DOBTextBlock.Text = Trainee.BirthDay.ToString("mm/dd/yyy");
            this.DataContext = this.Trainee;
        }

        private void EditTrainee_Button(object sender, RoutedEventArgs e)
        {
            EditTraineePage edit = new EditTraineePage(Trainee.ID);
            this.NavigationService.Navigate(edit);
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
                Home home = new Home();
                this.NavigationService.Navigate(home);
            }
        }

        
    }
}
