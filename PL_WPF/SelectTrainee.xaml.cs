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
        IBL BL = FactoryBL.getInstance();
        List<Trainee> trainees;

        public String SelectedID { get; set; }

        public SelectTrainee()
        {
            InitializeComponent();
            trainees = BL.getAllTrainees();
            Trainees.ItemsSource = trainees;
        }

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            //((AddTest)Owner).TraineeIDTextBox.Text = ((Button)sender).Tag.ToString();
            SelectedID = ((Button)sender).Tag.ToString();
            this.Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LevenshteinSearh_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(((TextBox)sender).Text))
            {
                Trainees.ItemsSource = trainees;
                return;
            }

            List<Trainee> tmp = new List<Trainee>();
            String querry = ((TextBox)sender).Text;

            foreach (Trainee t in trainees)
            {
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
