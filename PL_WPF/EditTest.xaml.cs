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
    /// Interaction logic for EditTest.xaml
    /// </summary>
    public partial class EditTest : Window
    {
        Test test;
        IBL BL;
        public EditTest(Test test)
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
            BL.updateTest(test);
            this.Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
