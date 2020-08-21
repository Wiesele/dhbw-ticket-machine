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
    /// <summary>
    /// Viewmodel for all customer views (Buying events and listing tickets
    /// </summary>
    public class CustomerMainViewModel: BindableBase
    {
        // --- Properties for frontend
        private ObservableCollection<Event> _events;
        public ObservableCollection<Event> Events { get { return this._events; } set { SetProperty(ref _events, value); } }

        private Customer _selectedCustomer;
        public Customer SelectedCustomer { get { return this._selectedCustomer; } set { SetProperty(ref _selectedCustomer, value); } }

        public Event _selectedEvent;
        public Event SelectedEvent { get { return this._selectedEvent; } set { SetProperty(ref _selectedEvent, value);  } }

        private int _selectedAmount;
        public int SelectedAmount { get { return this._selectedAmount; } set { SetProperty(ref _selectedAmount, value); this.CalcButtonActive(); } }

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

        /// <summary>
        /// Initialize lists and set default values
        /// </summary>
        /// <param name="selectedCustomer">Selected customer to load view for</param>
        public CustomerMainViewModel(Customer selectedCustomer)
        {
            this.Events = new ObservableCollection<Event>();
            this.Tickets = new ObservableCollection<Ticket>();
            this.SelectedCustomer = selectedCustomer;
        }

        /// <summary>
        /// Calculate if button to boy tickets is active
        /// </summary>
        public void CalcButtonActive()
        {
            // Button aktiv when:
                // Ticket selected
                // Ticket currently selling (date)
                // Tickets are available
                // A Amount ov tickets to buy has been selected
            this.ButtonActive = this.SelectedEvent != null && this.SelectedEvent.IsCurrentlySelling() && this.SelectedEvent.AvailableTickets > 0 && this.SelectedAmount > 0;
        }

        /// <summary>
        /// Loads all data for selected customer (events and tickets)
        /// </summary>
        public async void LoadData()
        {
            this.Events = new ObservableCollection<Event>();

            var actor = MainWindow.ActorSystem.ActorOf<AdministrationActor>();
            var task = actor.Ask(TransactionType.GetAllEvents);

            var events = await task as IEnumerable<Event>;

            this.Events.AddRange(events);
        }

        /// <summary>
        /// Update all values for customer (Budget, Tickets)
        /// </summary>
        /// <returns></returns>
        public async Task<Customer> UpdateCustomerData()
        {
            this.Events = new ObservableCollection<Event>();

            // Ask administration for Customerdata
            var actor = MainWindow.ActorSystem.ActorOf<AdministrationActor>();
            var task = actor.Ask(TransactionType.GetAllCustomer);

            var data = await task as IEnumerable<Customer>;
            
            // Select correct customer
            var customers = data.ToList();
            var customer = customers.Find(e => e.ID == this.SelectedCustomer.ID);

            this.SelectedCustomer = customer;

            // Load all tickets
            var collection = new ObservableCollection<Ticket>();
            collection.AddRange(customer.Tickets);
            this.AllTickets = collection;

            // Apply selected filter
            this.FilterEvents();

            return customer;
        }
        /// <summary>
        /// Buys selected ticket 
        /// </summary>
        /// <returns></returns>
        public async Task BuyTickets()
        {
            if(this.SelectedEvent == null)
            {
                this.CalcButtonActive();
                return;
            }

            // set ticket data
            var ticket = new Ticket()
            {
                Amount = this.SelectedAmount,
                EventId = this.SelectedEvent.ID,
            };

            // create transaction
            var transaction = new BuyTicketTransaction()
            {
                Ticket = ticket,
                CustomerId = this.SelectedCustomer.ID
            };

            // tell administration to buy ticket
            var actor = MainWindow.ActorSystem.ActorOf<AdministrationActor>();
            actor.Tell(transaction);
        }

        /// <summary>
        /// Filter list of events to selected filter values
        /// </summary>
        public void FilterEvents()
        {
            IEnumerable<Ticket> holder = this.AllTickets;


            if (this.FilterSellDate.HasValue)
            {
                holder = holder.Where(e => e.BoughtDate.Date == this.FilterSellDate.Value.Date);
            }
            if (this.FilterDate.HasValue)
            {
                holder = holder.Where(e => e.Event.Date.Date == this.FilterDate.Value.Date);
            }

            var obs = new ObservableCollection<Ticket>();
            obs.AddRange(holder);
            this.Tickets = obs;


        }
    }
}
