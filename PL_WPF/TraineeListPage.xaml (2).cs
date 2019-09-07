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
using BE;
using BL;
namespace PL_WPF
{
    /// <summary>
    /// Interaction logic for TraineeList.xaml
    /// </summary>
    public partial class TraineeListPage : Page
    {
        // data members
        IBL BL = FactoryBL.getInstance();
        List<Trainee> trainees;

        #region Constructor
        public TraineeListPage()
        {
            InitializeComponent();
            trainees = BL.getAllTrainees();
            Trainees.ItemsSource = trainees;
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshList_Click(object sender, RoutedEventArgs e)
        {
            Trainees.ItemsSource = BL.getAllTrainees();
            TraineeListPage refresh = new TraineeListPage();
            this.NavigationService.Navigate(refresh);
        }
        /// <summary>
        /// allow user to select the trainee they want to view and edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewandEditButton_Click(object sender, RoutedEventArgs e)
        {
            // define the trainee to view in the view trainee page
            ViewTraineePage view = new ViewTraineePage((String)((Button)sender).Tag);
            this.NavigationService.Navigate(view);
        }

        /// <summary>
        /// Levenshtein algorithm is used to determne which trainees are displayed as the user types in the text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LevenshteinSearh_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(((TextBox)sender).Text))
            {
                Trainees.ItemsSource = trainees;
                return;
            }

            List<Trainee> tmp = new List<Trainee>();
            String query = ((TextBox)sender).Text;
            // as each letter is entered into the textbox, calcualte the new word's distance from the other properties of the listed trainees
            foreach (Trainee t in trainees)
            {
                if (LevenshteinDistance.Calculate(query, t.ID) < LevenshteinDistance.len(query, t.ID) + 2 ||
                    LevenshteinDistance.Calculate(query, t.FirstName) < LevenshteinDistance.len(query, t.FirstName) + 3 ||
                    LevenshteinDistance.Calculate(query, t.LastName) < LevenshteinDistance.len(query, t.LastName) + 3)
                {
                    tmp.Add(t);
                }
            }

            Trainees.ItemsSource = tmp;
        }
    }
}
