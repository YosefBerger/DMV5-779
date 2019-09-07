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
    /// Interaction logic for Pages.xaml
    /// </summary>
    public partial class Pages : Window
    {
        public Pages()
        {
            InitializeComponent();
        }
        private void Button_ClickAddTrainees(object sender, RoutedEventArgs e)
        {
            Main.Content = new AddTraineePage();

        }

        private void Button_ClickAddTestPage(object sender, RoutedEventArgs e)
        {
            Main.Content = new AddTestPage();

        }

        private void Button_ClickHomePage(object sender, RoutedEventArgs e)
        {
            Main.Content = new HomePage();
        }
        private void Button_ClickViewTraineesList(object sender, RoutedEventArgs e)
        {
            Main.Content = new TraineeListPage();
        }

        private void Button_ClickContactUs(object sender, RoutedEventArgs e)
        {
            Main.Content = new ContactUs();
        }

        private void Button_ClickOurMissionPage(object sender, RoutedEventArgs e)
        {
            Main.Content = new OurMissionPage();
        }

        private void Button_ClickViewTestersList(object sender, RoutedEventArgs e)
        {
            Main.Content = new TestersListPage();
        }

        private void Button_ClickAddTesterPage(object sender, RoutedEventArgs e)
        {
            Main.Content = new AddTesterPage();
        }

        private void Button_ClickViewTestsList(object sender, RoutedEventArgs e)
        {
            Main.Content = new TestsListPage();
        }
    }
}
