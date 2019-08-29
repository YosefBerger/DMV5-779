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

namespace PL_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddTraineeBtn_Click(object sender, RoutedEventArgs e)
        {
            Window wnd = new AddTraineeWindow();
            wnd.Show();
        }

        private void TestersButton_Click(object sender, RoutedEventArgs e)
        {
            TestersList testersList = new TestersList()
            {
                Owner = this
            };
            testersList.ShowDialog();
        }

        private void TraineesButton_Click(object sender, RoutedEventArgs e)
        {
            TraineesList traineesList = new TraineesList()
            {
                Owner = this
            };
            traineesList.ShowDialog();
        }

        private void TestsButton_Click(object sender, RoutedEventArgs e)
        {
            TestsList testsList = new TestsList
            {
                Owner = this
            };
            testsList.ShowDialog();
        }
    }
}
