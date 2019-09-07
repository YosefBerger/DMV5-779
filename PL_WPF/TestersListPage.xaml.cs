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
    /// Interaction logic for TestersListPage.xaml
    /// </summary>
    public partial class TestersListPage : Page
    {
        private IBL BL;
        List<Tester> testers;
        #region Constructor
        public TestersListPage()
        {
            InitializeComponent();
            BL = FactoryBL.getInstance();
            testers = BL.getAllTesters();
            Testers.ItemsSource = testers;
        }
        #endregion


        private void ViewandEdt_Btn(object sender, RoutedEventArgs e)
        {
            ViewTesterPage view = new ViewTesterPage((String)((Button)sender).Tag);
            this.NavigationService.Navigate(view);
        }

        private void LevenshteinSearh_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(((TextBox)sender).Text))
            {
                Testers.ItemsSource = testers;
                return;
            }

            List<Tester> tmp = new List<Tester>();
            String querry = ((TextBox)sender).Text;

            foreach (Tester t in testers)
            {
                if (LevenshteinDistance.Calculate(querry, t.ID) < LevenshteinDistance.len(querry, t.ID) + 2 ||
                    LevenshteinDistance.Calculate(querry, t.FirstName) < LevenshteinDistance.len(querry, t.FirstName) + 3 ||
                    LevenshteinDistance.Calculate(querry, t.LastName) < LevenshteinDistance.len(querry, t.LastName) + 3)
                {
                    tmp.Add(t);
                }
            }

            Testers.ItemsSource = tmp;
        }
    }
}
