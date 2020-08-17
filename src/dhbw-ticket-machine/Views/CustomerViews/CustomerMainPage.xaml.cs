using dhbw_ticket_machine.Actors;
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

            AdministrationActor.EventDataChange += EventDataChange;
            AdministrationActor.CustomerDataChange += CustomerDataChange;
        }

        private void EventDataChange(object sender, IEnumerable<Event> events)
        {
            this.Dispatcher.Invoke(this.vm.LoadData);
        }

        private void CustomerDataChange(object sender, Customer customer)
        {
            this.Dispatcher.Invoke(this.UpdateBudget);
        }

        private async void UpdateBudget()
        {
            await this.vm.UpdateCustomerData();
            BudgetLabel.Content = this.vm.SelectedCustomer.Budget;
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
            if(vm.SelectedEvent == null)
            {
                return;
            }
            if(vm.SelectedAmount > vm.SelectedEvent.AvailableTickets)
            {
                vm.SelectedAmount = vm.SelectedEvent.AvailableTickets;
            }
            this.Slider_ValueChanged(AmountSlider, null);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(sender is Slider s)
            {
                if(this.vm.SelectedEvent != null)
                {
                    var max =  this.vm.SelectedCustomer.Budget / this.vm.SelectedEvent.Price;
                    if((int)max < this.vm.SelectedEvent.AvailableTickets)
                    {
                        s.Maximum = (int)max;
                    }
                    else
                    {
                        s.Maximum = this.vm.SelectedEvent.AvailableTickets;

                    }
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ShowTickets(this.vm.SelectedCustomer));
        }
    }
}
