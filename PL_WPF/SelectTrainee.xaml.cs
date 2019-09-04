using BE;
using BL;
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
    /// Interaction logic for SelectTrainee.xaml
    /// </summary>
    public partial class SelectTrainee : Window
    {
        IBL BL = FactoryBL.getInstance();

        public String SelectedID { get; set; }

        public SelectTrainee()
        {
            InitializeComponent();
            Trainees.ItemsSource = BL.getAllTrainees();
        }

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            //((AddTest)Owner).TraineeIDTextBox.Text = ((Button)sender).Tag.ToString();
            SelectedID = ((Button)sender).Tag.ToString();
            this.Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
