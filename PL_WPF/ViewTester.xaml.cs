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
    /// Interaction logic for ViewTester.xaml
    /// </summary>
    public partial class ViewTester : Window
    {
        Tester Tester;
        IBL BL;
        public ViewTester()
        {
            InitializeComponent();
            this.DataContext = this.Tester;
            DOBTextBlock.Text = Tester.BirthDay.ToString("mm/dd/yyy");
        }
        public ViewTester(Tester tester)
        {
            InitializeComponent();

            BL = FactoryBL.getInstance();
            Tester = tester.Clone();
            DOBTextBlock.Text = Tester.BirthDay.ToString("mm/dd/yyy");
            this.DataContext = this.Tester;
        }
        public ViewTester(String ID)
        {
            InitializeComponent();

            BL = FactoryBL.getInstance();
            Tester = BL.getAllTesters(new Func<Tester, bool>(t => t.ID == ID)).FirstOrDefault();
            DOBTextBlock.Text = Tester.BirthDay.ToString("mm/dd/yyy");
            this.DataContext = this.Tester;
        }

        private void EditTester_Button(object sender, RoutedEventArgs e)
        {
            //EditTester editTester = new EditTester(Tester.ID)
            //{
            //    Owner = this
            //};
            //editTester.Show();
        }

        private void Delete_Button(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you would like to delete the information for " + Tester.FirstName + " " + Tester.LastName + "?\nThis action cannot be undone!",
                                                      "Delete Tester", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                if (!BL.removeTester(Tester))
                {
                    MessageBox.Show("An error removing occured", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                this.Close();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
