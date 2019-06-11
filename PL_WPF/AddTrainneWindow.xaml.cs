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
    /// Interaction logic for AddTrainneWindow.xaml
    /// </summary>
    public partial class AddTrainneWindow : Window
    {
        public AddTrainneWindow()
        {
            InitializeComponent();
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
