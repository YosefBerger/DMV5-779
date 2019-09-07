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
        public bool IsCanceled { get; set; }
        BackgroundWorker getTestersWorker;
        Test test;
        List<Tester> testers;
        #region Constructors
        public SelectTester()
        {
            InitializeComponent();
            
            Spinner.Visibility = Visibility.Hidden;
            loadingText.Visibility = Visibility.Hidden;
            testers = BL.getAllTesters();
            Testers.ItemsSource = testers;
            IsCanceled = true;
        }
        public SelectTester(Test test)
        {
            this.test = test;
            IsCanceled = true;
            InitializeComponent();
            //List<Tester> testers = BL.getAllTesters(new Func<Tester, bool>(t => t.getIfWorking(test.DateTime) && t.));

            SearchIcon.Visibility = Visibility.Hidden;
            LevenshteinSearh.Visibility = Visibility.Hidden;
            Testers.Visibility = Visibility.Hidden;

            getTestersWorker = new BackgroundWorker();
            getTestersWorker.DoWork += new DoWorkEventHandler(GetValidTesters);
            getTestersWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(GetTestersRunWorkerCompleted);
            getTestersWorker.WorkerReportsProgress = false;
            getTestersWorker.WorkerSupportsCancellation = true;
            getTestersWorker.RunWorkerAsync(test);
        }
        #endregion

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
                this.Close();
                return;
            }

            Spinner.Visibility = Visibility.Hidden;
            loadingText.Visibility = Visibility.Hidden;

            testers = (List<Tester>)e.Result;
            Testers.ItemsSource = testers;
            SearchIcon.Visibility = Visibility.Hidden;
            LevenshteinSearh.Visibility = Visibility.Hidden;
            Testers.Visibility = Visibility.Visible;
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
                    IsCanceled = false;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LevenshteinSearh_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(((TextBox)sender).Text))
            {
                Testers.ItemsSource = testers;
                return;
            }

            List<Tester> tmp = new List<Tester>();
            String querry = ((TextBox)sender).Text;

            foreach (Tester t in testers)
            {
                if (LevenshteinDistance.Calculate(querry, t.ID) < LevenshteinDistance.len(querry, t.ID) + 2 ||
                    LevenshteinDistance.Calculate(querry, t.FirstName) < LevenshteinDistance.len(querry, t.FirstName) + 3 ||
                    LevenshteinDistance.Calculate(querry, t.LastName) < LevenshteinDistance.len(querry, t.LastName) + 3)
                {
                    tmp.Add(t);
                }
            }

            Testers.ItemsSource = tmp;
        }
    }
}
