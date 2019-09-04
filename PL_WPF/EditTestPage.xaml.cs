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
    /// Interaction logic for EditTestPage.xaml
    /// </summary>
    public partial class EditTestPage : Page
    {
        Test test;
        IBL BL;
        public EditTestPage(Test test)
        {
            InitializeComponent();
            this.test = test.Clone();
            this.DataContext = this.test;
            TestDate.Text = test.DateTime.ToString("mm/dd/yyyy");
            TestHour.Text = test.DateTime.Hour + ":00";
            BL = FactoryBL.getInstance();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (BL.updateTest(test))
            {
                HomePage HomePage = new HomePage();
                this.NavigationService.Navigate(HomePage);
            }

            MessageBox.Show("An error occured", "Delete Test", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            HomePage HomePage = new HomePage();
            this.NavigationService.Navigate(HomePage);
        }
    }
}
