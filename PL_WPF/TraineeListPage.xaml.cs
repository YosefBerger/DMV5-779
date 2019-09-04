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
using BE;
using BL;
namespace PL_WPF
{
    /// <summary>
    /// Interaction logic for TraineeList.xaml
    /// </summary>
    public partial class TraineeListPage : Page
    {
        IBL BL = FactoryBL.getInstance();
        public TraineeListPage()
        {
            InitializeComponent();
            Trainees.ItemsSource = BL.getAllTrainees();
        }
        

       
       

        private void RefreshList_Click(object sender, RoutedEventArgs e)
        {
            Trainees.ItemsSource = BL.getAllTrainees();
            TraineeListPage refresh = new TraineeListPage();
            this.NavigationService.Navigate(refresh);
        }

        private void ViewandEditButton_Click(object sender, RoutedEventArgs e)
        {
            ViewTraineePage view = new ViewTraineePage((String)((Button)sender).Tag);
            this.NavigationService.Navigate(view);
        }
    }
}
