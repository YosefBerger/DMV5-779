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
    /// Interaction logic for TestersList.xaml
    /// </summary>
    public partial class TestersList : Window
    {
        IBL BL = FactoryBL.getInstance();
        public TestersList()
        {
            InitializeComponent();
            Testers.ItemsSource = BL.getAllTesters();
        }

        private void AddTesterButton_Click(object sender, RoutedEventArgs e)
        {
            AddTester addTester = new AddTester()
            {
                Owner = this
            };

            addTester.ShowDialog();
        }

        private void ViewandEdt_Btn(object sender, RoutedEventArgs e)
        {
            ViewTester viewTester = new ViewTester((string)((Button)sender).Tag)
            {
                Owner = this
            };
            viewTester.ShowDialog();
        }

        private void RefreshList_Click(object sender, RoutedEventArgs e)
        {
            Testers.ItemsSource = BL.getAllTesters();
        }
    }
}
