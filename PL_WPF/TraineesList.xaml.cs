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
    /// Interaction logic for TraineesList.xaml
    /// </summary>
    public partial class TraineesList : Window
    {
        public TraineesList()
        {
            InitializeComponent();
        }

        private void TextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            
        }

        private void AddTrainee_Btn(object sender, RoutedEventArgs e)
        {
            AddTraineeWindow addTrainee = new AddTraineeWindow();
            addTrainee.Owner = this;
            addTrainee.ShowDialog();
        }

        private void ViewandEdt_Btn(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
