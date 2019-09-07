using BE;
using BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for SugestDate.xaml
    /// </summary>
    public partial class SugestDate : Window
    {
        IBL BL;
        BackgroundWorker getDate;

        #region Constructors
        public SugestDate()
        {
            InitializeComponent();
        }
        public SugestDate(Test test)
        {
            BL = FactoryBL.getInstance();
            InitializeComponent();

            // Hide the text box
            DepositTextBox.Visibility = Visibility.Hidden;
            
            GetDate(test);
        }
        #endregion

        /// <summary>
        /// Create and start a background worker to find a new test date
        /// </summary>
        /// <param name="test"></param>
        private void GetDate(Test test)
        {
            getDate = new BackgroundWorker();
            getDate.DoWork += new DoWorkEventHandler(getDateWorkerDoWork);
            getDate.RunWorkerCompleted += new RunWorkerCompletedEventHandler(getDateWorkerRunComplete);
            getDate.RunWorkerAsync(test);
        }

        /// <summary>
        /// Once date is found display it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void getDateWorkerRunComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            // Hide the spinner
            Spinner.Visibility = Visibility.Hidden;

            // Change the date
            TitleTextBlock.Text = "Sugested Date";
            // If somehting whent wrong close the window
            if(e.Result == null)
            {
                this.Close();
                return;
            }

            // Convert the date to a string and put it in the text box
            DepositTextBox.Text = ((DateTime)e.Result).ToString("mm/dd/yyyy");
            // Show the text box
            DepositTextBox.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Get the new date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void getDateWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            // If no test was passed just close
            if(e.Argument == null)
            {
                this.Close();
                return;
            }

            // Get the new test date
            e.Result = BL.NewValidDateTime(e.Argument as Test);
        }
        /// <summary>
        /// close the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
