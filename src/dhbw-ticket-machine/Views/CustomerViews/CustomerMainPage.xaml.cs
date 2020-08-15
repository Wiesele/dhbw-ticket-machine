using dhbw_ticket_machine.Models;
using dhbw_ticket_machine.ViewModels;
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
    /// Interaktionslogik für CustomerMainPage.xaml
    /// </summary>
    public partial class CustomerMainPage : Page
    {
        CustomerMainViewModel vm;
        public CustomerMainPage(Customer selectedCustomer)
        {
            this.vm = new CustomerMainViewModel(selectedCustomer);
            InitializeComponent();
            DataContext = vm;

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await this.vm.BuyTickets();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.vm.LoadData();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(vm.SelectedAmount > vm.SelectedEvent.AvailableTickets)
            {
                vm.SelectedAmount = vm.SelectedEvent.AvailableTickets;
            }
        }
    }
}
