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
        // data members
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

        /// <summary>
        /// Button takes the user to the view and edit tester page for the selected tester
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewandEdt_Btn(object sender, RoutedEventArgs e)
        {
            // pass the selected tester to the view testers page
            ViewTesterPage view = new ViewTesterPage((String)((Button)sender).Tag);
            this.NavigationService.Navigate(view);
        }

        /// <summary>
        /// filters the list of potential testers based on distance to the typed text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LevenshteinSearh_TextChanged(object sender, TextChangedEventArgs e)
        {
            // if no string, display everyone
            if (string.IsNullOrWhiteSpace(((TextBox)sender).Text))
            {
                Testers.ItemsSource = testers;
                return;
            }

            List<Tester> tmp = new List<Tester>();
            String quary = ((TextBox)sender).Text;

            // display all testers that are within the required distance
            foreach (Tester t in testers)
            {
                if (LevenshteinDistance.Calculate(quary, t.ID) < LevenshteinDistance.len(quary, t.ID) + 2 ||
                    LevenshteinDistance.Calculate(quary, t.FirstName) < LevenshteinDistance.len(quary, t.FirstName) + 3 ||
                    LevenshteinDistance.Calculate(quary, t.LastName) < LevenshteinDistance.len(quary, t.LastName) + 3)
                {
                    tmp.Add(t);
                }
            }

            Testers.ItemsSource = tmp;
        }
    }
}
