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
    /// Interaction logic for SelectTrainee.xaml
    /// </summary>
    public partial class SelectTrainee : Window
    {
        // Hold an IBL
        IBL BL = FactoryBL.getInstance();
        // Hold Trainees to select from
        List<Trainee> trainees;

        // This allows the parent page to get the selected trainee id
        public String SelectedID { get; set; }

        /// <summary>
        /// Present to the user a list of trainees and allow them to select one
        /// </summary>
        public SelectTrainee()
        {
            InitializeComponent();
            trainees = BL.getAllTrainees();
            Trainees.ItemsSource = trainees;
        }

        // Get the selected trainee id and close the window
        private void Select_Click(object sender, RoutedEventArgs e)
        {
            // Store the selected id
            SelectedID = ((Button)sender).Tag.ToString();

            // Close the window
            this.Close();
        }

        /// <summary>
        /// Provides fuzzy search capability
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LevenshteinSearh_TextChanged(object sender, TextChangedEventArgs e)
        {
            // If the sting in the search box is empy or is just whitespace, just return all the trainees
            if (string.IsNullOrWhiteSpace(((TextBox)sender).Text))
            {
                Trainees.ItemsSource = trainees;
                return;
            }

            // Hold the trainees to display
            List<Trainee> tmp = new List<Trainee>();
            // Get the querry to compare against
            String querry = ((TextBox)sender).Text;

            foreach (Trainee t in trainees)
            {
                // Compare the querry against the trainees ID, First name, and Last name
                if (LevenshteinDistance.Calculate(querry, t.ID) < LevenshteinDistance.len(querry, t.ID) + 2 ||
                    LevenshteinDistance.Calculate(querry, t.FirstName) < LevenshteinDistance.len(querry, t.FirstName) + 3 ||
                    LevenshteinDistance.Calculate(querry, t.LastName) < LevenshteinDistance.len(querry, t.LastName) + 3)
                {
                    tmp.Add(t);
                }
            }

            Trainees.ItemsSource = tmp;
        }
    }
}
