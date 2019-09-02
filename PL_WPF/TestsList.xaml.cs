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
    /// Interaction logic for TestsList.xaml
    /// </summary>
    public partial class TestsList : Window
    {
        List<Test> ItemsSource;
        IBL BL;
        public TestsList()
        {
            BL = FactoryBL.getInstance();
            InitializeComponent();
            ItemsSource = BL.getAllTests();
            ItemsSource.Sort();
            Tests.ItemsSource = ItemsSource;
        }

        private void ViewandEdt_Btn(object sender, RoutedEventArgs e)
        {
            Test test = BL.GetTestByNumber(((Button)sender).Tag.ToString());
            if (test != null)
            {
                ViewTest viewTest = new ViewTest(test)
                {
                    Owner = this
                };
                viewTest.ShowDialog();
            }
        }

        private void AddTestButton_Click(object sender, RoutedEventArgs e)
        {
            AddTest addTest = new AddTest()
            {
                Owner = this
            };
            addTest.ShowDialog();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RefreshList_Click(object sender, RoutedEventArgs e)
        {
            ItemsSource = BL.getAllTests();
            ItemsSource.Sort();
            Tests.ItemsSource = ItemsSource;
        }
    }
}
