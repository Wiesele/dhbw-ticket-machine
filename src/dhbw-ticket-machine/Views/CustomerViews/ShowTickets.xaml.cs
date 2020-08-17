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
    /// Interaktionslogik für ShowTickets.xaml
    /// </summary>
    public partial class ShowTickets : Page
    {
        CustomerMainViewModel vm;
        public ShowTickets(Customer selectedCustomer)
        {
            InitializeComponent();
            vm = new CustomerMainViewModel(selectedCustomer);
            this.DataContext = this.vm;
            this.UpdateBudget();
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

    }
}
