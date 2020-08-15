using Akka.Actor;
using dhbw_ticket_machine.actors.TransactionTypes;
using dhbw_ticket_machine.Actors;
using dhbw_ticket_machine.Extensions;
using dhbw_ticket_machine.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace dhbw_ticket_machine.ViewModels
{
    public class SelectCustomerViewModel: BindableBase
    {
        public SelectCustomerViewModel()
        {
            this.Customer = new ObservableCollection<Customer>();
            this.LoadData();
        }

        private Customer _selectedCustomer;
        public Customer SelectedCustomer { get { return this._selectedCustomer; } set { SetProperty(ref this._selectedCustomer, value); } }


        private ObservableCollection<Customer> _customer;
        public ObservableCollection<Customer> Customer { get { return this._customer; } set { SetProperty(ref _customer, value); } }

        public async void LoadData()
        {
            var actor = MainWindow.ActorSystem.ActorOf<AdministrationActor>();
            var values = actor.Ask(TransactionType.GetAllCustomer);

            this.Customer.AddRange((IEnumerable<Customer>)await values);
        }
    }
}
