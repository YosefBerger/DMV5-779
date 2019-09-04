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
    /// Interaction logic for ViewTesterPage.xaml
    /// </summary>
    public partial class ViewTesterPage : Page
    {
        Tester Tester;
        IBL BL;
        public ViewTesterPage()
        {
            InitializeComponent();
            this.DataContext = this.Tester;
            DOBTextBlock.Text = Tester.BirthDay.ToString("mm/dd/yyy");
        }
        public ViewTesterPage(Tester tester)
        {
            InitializeComponent();

            BL = FactoryBL.getInstance();
            Tester = tester.Clone();
            DOBTextBlock.Text = Tester.BirthDay.ToString("mm/dd/yyy");
            this.DataContext = this.Tester;
        }
        public ViewTesterPage(String ID)
        {
            InitializeComponent();

            BL = FactoryBL.getInstance();
            Tester = BL.getAllTesters(new Func<Tester, bool>(t => t.ID == ID)).FirstOrDefault();
            DOBTextBlock.Text = Tester.BirthDay.ToString("mm/dd/yyy");
            this.DataContext = this.Tester;
        }

        private void EditTester_Button(object sender, RoutedEventArgs e)
        {
            EditTesterPage edit = new EditTesterPage(Tester.ID);
            this.NavigationService.Navigate(edit);

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
                Home home = new Home();
                this.NavigationService.Navigate(home);
            }
        }

       
    }
}
