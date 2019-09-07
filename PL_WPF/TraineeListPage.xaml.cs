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



        private void RefreshList_Click(object sender, RoutedEventArgs e)
        {
            Trainees.ItemsSource = BL.getAllTrainees();
            TraineeListPage refresh = new TraineeListPage();
            this.NavigationService.Navigate(refresh);
        }

        private void ViewandEditButton_Click(object sender, RoutedEventArgs e)
        {
            ViewTraineePage view = new ViewTraineePage((String)((Button)sender).Tag);
            this.NavigationService.Navigate(view);
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
