﻿using BL;
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
        IBL BL = FactoryBL.getInstance();
        public TraineesList()
        {
            InitializeComponent();
            Trainees.ItemsSource = BL.getAllTrainees();
        }

        private new void Activated(object sender, EventArgs e)
        {
            Trainees.ItemsSource = BL.getAllTrainees();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            Trainees.ItemsSource = BL.getAllTrainees();
            this.Owner.Hide();
        }
        private new void Closed(object sender, RoutedEventArgs e)
        {
            this.Owner.Show();
        }

        private void AddTrainee_Btn(object sender, RoutedEventArgs e)
        {
            AddTrainee addTrainee = new AddTrainee
            {
                Owner = this
            };
            addTrainee.ShowDialog();
        }

        private void ViewandEdt_Btn(object sender, RoutedEventArgs e)
        {
            ViewTrainee viewTrainee = new ViewTrainee((string)((Button)sender).Tag)
            {
                Owner = this
            };
            viewTrainee.ShowDialog();
        }

        private void RefreshList_Click(object sender, RoutedEventArgs e)
        {
            Trainees.ItemsSource = BL.getAllTrainees();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
