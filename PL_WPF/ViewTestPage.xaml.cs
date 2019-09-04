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
    /// Interaction logic for ViewTestPage.xaml
    /// </summary>
    public partial class ViewTestPage : Page
    {
        Test test;
        IBL BL;
        public ViewTestPage()
        {
            InitializeComponent();
        }
        public ViewTestPage(Test test)
        {
            this.test = test;
            InitializeComponent();
            this.DataContext = this.test;
            TestDate.Text = test.DateTime.ToString("mm/dd/yyyy");
            TestHour.Text = test.DateTime.Hour + ":00";
            BL = FactoryBL.getInstance();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            EditTestPage edit = new EditTestPage(test);
            this.NavigationService.Navigate(edit);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you would like to delete this test?\nThis cation cannot be undone.", "Confirm Cancelation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                bool tmp = BL.removeTest(test);

                MessageBox.Show(tmp ? "The test was removed." : "An error occured", "Delete Test", MessageBoxButton.OK, tmp ? MessageBoxImage.Asterisk : MessageBoxImage.Error);

                Home home = new Home();
                this.NavigationService.Navigate(home);
            }
        }
    }
}
