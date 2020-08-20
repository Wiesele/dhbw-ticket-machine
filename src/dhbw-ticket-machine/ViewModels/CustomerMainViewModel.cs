using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Akka.Actor;
using Akka.Actor.Dsl;
using Akka.Dispatch;
using dhbw_ticket_machine.actors.TransactionTypes;
using dhbw_ticket_machine.Actors;
using dhbw_ticket_machine.Actors.TransactionTypes;
using dhbw_ticket_machine.Extensions;
using dhbw_ticket_machine.Models;

namespace dhbw_ticket_machine.ViewModels
{
    public class CustomerMainViewModel: BindableBase
    {
        private ObservableCollection<Event> _events;
        public ObservableCollection<Event> Events { get { return this._events; } set { SetProperty(ref _events, value); } }

        private Customer _selectedCustomer;
        public Customer SelectedCustomer { get { return this._selectedCustomer; } set { SetProperty(ref _selectedCustomer, value); } }

        public Event _selectedEvent;
        public Event SelectedEvent { get { return this._selectedEvent; } set { SetProperty(ref _selectedEvent, value); this.ButtonActive = value != null && value.IsCurrentlySelling() && value.AvailableTickets > 0; } }

        private int _selectedAmount;
        public int SelectedAmount { get { return this._selectedAmount; } set { SetProperty(ref _selectedAmount, value); } }

        private bool _buttonActive;
        public bool ButtonActive { get { return this._buttonActive; } set { SetProperty(ref _buttonActive, value); } }


        private ObservableCollection<Ticket> _tickets;
        public ObservableCollection<Ticket> Tickets { get { return this._tickets; } set { SetProperty(ref _tickets, value); } }

        private ObservableCollection<Ticket> _allTickets;
        public ObservableCollection<Ticket> AllTickets { get { return this._allTickets; } set { SetProperty(ref _allTickets, value); } }


        private DateTime? _filterDate;
        public DateTime? FilterDate { get { return this._filterDate; } set { SetProperty(ref _filterDate, value); } }


        private DateTime? _filterSellDate;
        public DateTime? FilterSellDate { get { return this._filterSellDate; } set { SetProperty(ref _filterSellDate, value); } }

        public CustomerMainViewModel(Customer selectedCustomer)
        {
            this.Events = new ObservableCollection<Event>();
            this.Tickets = new ObservableCollection<Ticket>();
            this.ResetTextFields();
            this.SelectedCustomer = selectedCustomer;
        }


        public async void LoadData()
        {
            this.Events = new ObservableCollection<Event>();

            var actor = MainWindow.ActorSystem.ActorOf<AdministrationActor>();
            var task = actor.Ask(TransactionType.GetAllEvents);

            var events = await task as IEnumerable<Event>;

            this.Events.AddRange(events);
        }

        public async Task<Customer> UpdateCustomerData()
        {
            this.Events = new ObservableCollection<Event>();

            var actor = MainWindow.ActorSystem.ActorOf<AdministrationActor>();
            var task = actor.Ask(TransactionType.GetAllCustomer);

            var data = await task as IEnumerable<Customer>;

            var customers = data.ToList();
            var customer = customers.Find(e => e.ID == this.SelectedCustomer.ID);

            this.SelectedCustomer = customer;

            var collection = new ObservableCollection<Ticket>();
            collection.AddRange(customer.Tickets);
            this.AllTickets = collection;

            this.FilterEvents();

            return customer;
        }

        public async void ResetTextFields()
        {
        }

        public async Task BuyTickets()
        {
            var ticket = new Ticket()
            {
                Amount = this.SelectedAmount,
                EventId = this.SelectedEvent.ID,
            };

            var transaction = new BuyTicketTransaction()
            {
                Ticket = ticket,
                CustomerId = this.SelectedCustomer.ID
            };

            //var newEvent = new Event()
            //{
            //    Date = this.NewDate,
            //    Location = this.NewLocation,
            //    Name = this.NewName,
            //    Price = this.NewPrice,
            //    DaysBeforSalesStart = this.DaysBefore,
            //    TotalTicketAmount = this.TicketAmount,
            //    ID = Guid.NewGuid()
            //};

            //if (!newEvent.IsValid())
            //{
            //    var msg = MessageBox.Show("Nicht alle Felder wurden gefüllt!", "Warnung!", MessageBoxButton.OK);
            //    return;
            //}


            var actor = MainWindow.ActorSystem.ActorOf<AdministrationActor>();
            actor.Tell(transaction);
            //// Wait for Item to be inserted
            //var task = actor.Ask(newEvent);
            //await task;

            //// Reload List
            //this.LoadData();

            //this.ResetTextFields();
        }

        public void FilterEvents()
        {
            IEnumerable<Ticket> holder = this.AllTickets;

            var changed = false;

            if (this.FilterSellDate.HasValue)
            {
                holder = holder.Where(e => e.BoughtDate.Date == this.FilterSellDate.Value.Date);
                changed = true;
            }
            if (this.FilterDate.HasValue)
            {
                holder = holder.Where(e => e.Event.Date == this.FilterDate.Value.Date);
                changed = true;
            }

            var obs = new ObservableCollection<Ticket>();
            obs.AddRange(holder);
            this.Tickets = obs;


        }
    }
}
