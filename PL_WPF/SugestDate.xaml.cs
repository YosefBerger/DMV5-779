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
        public SugestDate()
        {
            InitializeComponent();
        }
        public SugestDate(Test test)
        {
            BL = FactoryBL.getInstance();
            InitializeComponent();
            DepositTextBox.Visibility = Visibility.Hidden;
            
            GetDate(test);
        }

        private void GetDate(Test test)
        {
            getDate = new BackgroundWorker();
            getDate.DoWork += new DoWorkEventHandler(getDateWorkerDoWork);
            getDate.RunWorkerCompleted += new RunWorkerCompletedEventHandler(getDateWorkerRunComplete);
            getDate.RunWorkerAsync(test);
        }

        private void getDateWorkerRunComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            Spinner.Visibility = Visibility.Hidden;
            TitleTextBlock.Text = "Sugested Date";
            if(e.Result == null)
            {
                this.Close();
                return;
            }
            DepositTextBox.Text = ((DateTime)e.Result).ToString("mm/dd/yyyy");
            DepositTextBox.Visibility = Visibility.Visible;
        }

        private void getDateWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            if(e.Argument == null)
            {
                this.Close();
                return;
            }

            e.Result = BL.NewValidDateTime(e.Argument as Test);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
