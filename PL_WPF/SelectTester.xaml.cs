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
    /// Interaction logic for SelectTester.xaml
    /// </summary>
    public partial class SelectTester : Window
    {
        IBL BL = FactoryBL.getInstance();
        BackgroundWorker getTestersWorker;
        Test test;

        public SelectTester()
        {
            InitializeComponent();
            gettingTesterProgressBar.Visibility = Visibility.Hidden;
            loadingText.Visibility = Visibility.Hidden;
            Testers.ItemsSource = BL.getAllTesters();
        }
        public SelectTester(Test test)
        {
            this.test = test;
            InitializeComponent();
            //List<Tester> testers = BL.getAllTesters(new Func<Tester, bool>(t => t.getIfWorking(test.DateTime) && t.));

            Testers.Visibility = Visibility.Hidden;

            getTestersWorker = new BackgroundWorker();
            getTestersWorker.DoWork += new DoWorkEventHandler(GetValidTesters);
            getTestersWorker.ProgressChanged += new ProgressChangedEventHandler(GetTestersProgressChanged);
            getTestersWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(GetTestersRunWorkerCompleted);
            getTestersWorker.WorkerReportsProgress = true;
            getTestersWorker.WorkerSupportsCancellation = true;
            getTestersWorker.RunWorkerAsync(test);
        }

        void GetTestersRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                this.Close();
            }

            // If there are no Testers for the suplied date
            if (((List<Tester>)e.Result).Count == 0)
            {
                MessageBoxResult result = MessageBox.Show("No Testers were found, would you like us to try and sugest a date and time?\nIt might take a while.", "No Testers", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    SugestDate sugestDate = new SugestDate(test);
                    sugestDate.ShowDialog();
                    this.Close();
                    return;
                }
            }

            gettingTesterProgressBar.Visibility = Visibility.Hidden;
            loadingText.Visibility = Visibility.Hidden;

            Testers.ItemsSource = (List<Tester>)e.Result;
            Testers.Visibility = Visibility.Visible;
            MessageBox.Show("Done");
        }

        void GetTestersProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            gettingTesterProgressBar.Value = e.ProgressPercentage;
        }

        void GetValidTesters (object sender, DoWorkEventArgs e)
        {
            Test test = (Test)e.Argument;
            Trainee trainee = BL.getAllTrainees(new Func<Trainee, bool>(tr => tr.ID == test.TraineeId)).FirstOrDefault();
            List<Tester> testers = BL.testersForVehicle(trainee.VehicleType);
            if (testers == null || testers.Count == 0)
            {
                Console.WriteLine("No Testers after Vehicle Type");
                e.Result = new List<Tester>();
                return;
            }
            if (((BackgroundWorker)sender).CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            testers = (from t in testers
                       where ((DateTime.Today - t.BirthDay).Days / 365) >= Configuration.TESTER_MIN_AGE
                       select t).ToList();
            if (testers == null || testers.Count == 0)
            {
                Console.WriteLine("No Testers after Age");
                e.Result = new List<Tester>();
                return;
            }
            if (((BackgroundWorker)sender).CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            testers = BL.testersForTime(test.DateTime, testers);
            if (testers == null || testers.Count == 0)
            {
                Console.WriteLine("No Testers after Time");
                e.Result = new List<Tester>();
                return;
            }
            Console.WriteLine("After Time we have " + testers.Count);
            if (((BackgroundWorker)sender).CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            testers = BL.testersForAddress(test.StartAddress, testers, (BackgroundWorker)sender);
            if (testers == null || testers.Count == 0)
            {
                Console.WriteLine("No Testers after Address");
                e.Result = new List<Tester>();
                return;
            }
            Console.WriteLine("After Address we have " + testers.Count);
            if (((BackgroundWorker)sender).CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            Console.WriteLine("Testers were found");
            e.Result = testers;
        }

        private void CancelLoadingButton_Click(object sender, RoutedEventArgs e)
        {
            if (getTestersWorker != null && getTestersWorker.IsBusy)
            {
                getTestersWorker.CancelAsync();
            }

            this.Close();
        }

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            test.TesterId = ((Button)sender).Tag.ToString();
            try
            {
                if (!BL.addTest(test))
                {
                    MessageBox.Show("Something went wrong adding the test");
                }
                else
                {
                    this.Owner.Close();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
