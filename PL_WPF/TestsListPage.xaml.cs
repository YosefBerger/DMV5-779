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
    /// Interaction logic for TestsListPage.xaml
    /// </summary>
    public partial class TestsListPage : Page
    {
        List<Test> tests;
        IBL BL;

        #region Constructor
        public TestsListPage()
        {
            BL = FactoryBL.getInstance();
            InitializeComponent();
            tests = BL.getAllTests();
            Tests.ItemsSource = tests;
        }
        #endregion

        private void ViewandEdt_Btn(object sender, RoutedEventArgs e)
        {
            Test test = BL.GetTestByNumber(((Button)sender).Tag.ToString());
            if (test != null)
            {
                ViewTestPage view = new ViewTestPage(test);
                this.NavigationService.Navigate(view);
            }
        }

        private void LevenshteinSearh_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(((TextBox)sender).Text))
            {
                Tests.ItemsSource = tests;
                return;
            }

            List<Test> tmp = new List<Test>();
            String querry = ((TextBox)sender).Text;

            if (querry.CompareTo("passed") == 0)
            {
                foreach (Test t in tests)
                {
                    if (t.Result)
                    {
                        tmp.Add(t);
                    }
                }
                Tests.ItemsSource = tmp;
                return;
            }

            foreach (Test t in tests)
            {
                if (LevenshteinDistance.Calculate(querry, t.TestNumber) < LevenshteinDistance.len(querry, t.TestNumber) + 2 ||
                    LevenshteinDistance.Calculate(querry, t.TesterId) < LevenshteinDistance.len(querry, t.TesterId) + 2 ||
                    LevenshteinDistance.Calculate(querry, t.TraineeId) < LevenshteinDistance.len(querry, t.TraineeId) + 2)
                {
                    tmp.Add(t);
                }
            }

            Tests.ItemsSource = tmp;
        }
    }
}
