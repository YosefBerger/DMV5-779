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
        //constructor
        public Pages()
        {
            InitializeComponent();
        }

        #region Buttons
        /// <summary>
        /// when add trainee button is clicked, add trainee page is opened
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickAddTrainees(object sender, RoutedEventArgs e)
        {
            Main.Content = new AddTraineePage();

        }

        /// <summary>
        /// button for the add test page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickAddTestPage(object sender, RoutedEventArgs e)
        {
            Main.Content = new AddTestPage();

        }
        
        /// <summary>
        /// home page button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickHomePage(object sender, RoutedEventArgs e)
        {
            Main.Content = new HomePage();
        }
        /// <summary>
        /// Trainee List button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickViewTraineesList(object sender, RoutedEventArgs e)
        {
            Main.Content = new TraineeListPage();
        }
        /// <summary>
        /// Contact Us button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickContactUs(object sender, RoutedEventArgs e)
        {
            Main.Content = new ContactUs();
        }

        /// <summary>
        /// Our Mission button Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickOurMissionPage(object sender, RoutedEventArgs e)
        {
            Main.Content = new OurMissionPage();
        }
        /// <summary>
        /// Vew Testers List button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickViewTestersList(object sender, RoutedEventArgs e)
        {
            Main.Content = new TestersListPage();
        }

        /// <summary>
        /// Add tester page button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickAddTesterPage(object sender, RoutedEventArgs e)
        {
            Main.Content = new AddTesterPage();
        }
        /// <summary>
        /// View Tests List page button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickViewTestsList(object sender, RoutedEventArgs e)
        {
            Main.Content = new TestsListPage();
        }
        #endregion Dova
    }
}
