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
        // data members
        Trainee Trainee;
        IBL BL;
        #region Constructors
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
        #endregion

        #region Buttons
        /// <summary>
        /// edit trainee button for the dispalyed trainee
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditTrainee_Button(object sender, RoutedEventArgs e)
        {
            // open up the Edit Trainee Page for the selected Trainee's ID
            EditTraineePage edit = new EditTraineePage(Trainee.ID);
            this.NavigationService.Navigate(edit);
        }

        /// <summary>
        /// Delete button for the displayed Trainee
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Button(object sender, RoutedEventArgs e)
        {
            // Message ox asking user if he is sure about deleting the trainee
            MessageBoxResult result = MessageBox.Show("Are you sure you would like to delete the information for " + Trainee.FirstName + " " + Trainee.LastName + "?\nThis action cannot be undone!",
                                                      "Delete Trainee", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                // if error occurs, the user is informed
                if (!BL.removeTrainee(Trainee))
                {
                    MessageBox.Show("An error removing occured", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                // Home page is opened
                HomePage HomePage = new HomePage();
                this.NavigationService.Navigate(HomePage);
            }
        }
        #endregion

    }
}
