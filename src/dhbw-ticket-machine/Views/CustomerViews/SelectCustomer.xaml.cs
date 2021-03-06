﻿using dhbw_ticket_machine.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace dhbw_ticket_machine.Views.CustomerViews
{
    /// <summary>
    /// Interaktionslogik für SelectCustomer.xaml
    /// </summary>
    public partial class SelectCustomer : Page
    {
        SelectCustomerViewModel vm = new SelectCustomerViewModel();
        public SelectCustomer()
        {
            InitializeComponent();
            DataContext = vm;
        }

        /// <summary>
        /// When customer is selected -> Load CustomerMainPage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(vm.SelectedCustomer != null)
            {
                NavigationService.Navigate(new CustomerMainPage(vm.SelectedCustomer));
            }
        }
    }
}
