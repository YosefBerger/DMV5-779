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
    /// Interaction logic for SelectTester.xaml
    /// </summary>
    public partial class SelectTester : Window
    {
        IBL BL = FactoryBL.getInstance();
        public SelectTester()
        {
            InitializeComponent();
            Testers.ItemsSource = BL.getAllTesters();
        }
        public SelectTester(Test test)
        {
            InitializeComponent();
            //List<Tester> testers = BL.getAllTesters(new Func<Tester, bool>(t => t.getIfWorking(test.DateTime) && t.));
        }
    }
}
