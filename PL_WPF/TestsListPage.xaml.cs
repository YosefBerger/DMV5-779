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
        IBL BL;
        public TestsListPage()
        {
            BL = FactoryBL.getInstance();
            InitializeComponent();
            Tests.ItemsSource = BL.getAllTests();
        }

        private void ViewandEdt_Btn(object sender, RoutedEventArgs e)
        {
            Test test = BL.GetTestByNumber(((Button)sender).Tag.ToString());
            if (test != null)
            {
                ViewTestPage view = new ViewTestPage(test);
                this.NavigationService.Navigate(view);
            }
        }

       

        
    }
}
