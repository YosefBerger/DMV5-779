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
        public TestersListPage()
        {
            InitializeComponent();
            BL = FactoryBL.getInstance();
            Testers.ItemsSource = BL.getAllTesters();
        }

       

        private void ViewandEdt_Btn(object sender, RoutedEventArgs e)
        {
            ViewTesterPage view = new ViewTesterPage((String)((Button)sender).Tag);
            this.NavigationService.Navigate(view);
        }

       
    }
}
