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
    /// Interaction logic for ViewTest.xaml
    /// </summary>
    public partial class ViewTest : Window
    {
        Test test;
        IBL BL;
        public ViewTest()
        {
            InitializeComponent();
        }
        public ViewTest(Test test)
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
            EditTest editTest = new EditTest(test)
            {
                Owner = this
            };
            editTest.ShowDialog();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you would like to delete this test?\nThis cation cannot be undone.", "Confirm Cancelation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                BL.removeTest(test);
                this.Close();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
