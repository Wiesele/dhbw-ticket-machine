using Akka.Actor;
using dhbw_ticket_machine.actors.TransactionTypes;
using dhbw_ticket_machine.Actors.TransactionTypes;
using dhbw_ticket_machine.Data;
using dhbw_ticket_machine.Extensions;
using dhbw_ticket_machine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace dhbw_ticket_machine.Actors
{
    /// <summary>
    /// Actor to manage database
    /// </summary>
    public class AdministrationActor: ReceiveActor
    {
        IDatabase database;


        //------ Events to sync data between clients
        public delegate void EventDataChangeHandler(object source, IEnumerable<Event> Events);
        public static event EventDataChangeHandler EventDataChange;

        public delegate void CustomerDataChangeHandler(object source, Customer Events);
        public static event CustomerDataChangeHandler CustomerDataChange;


        public AdministrationActor()
        {
            this.database = MockDatabase.Create();

            // When receiving a load all transaction
            Receive<TransactionType>(e =>
            {
                // Load all events 
                if(e == TransactionType.GetAllEvents)
                {
                    var returnValue = database.Events;
                    foreach (var item in returnValue)
                    {
                        item.SoldVolume = 0;
                    }
                    // Calculate sold tickets
                    foreach (var customer in database.Customers)
                    {
                        foreach (var ticket in customer.Tickets)
                        {
                            var holder = returnValue.Find(e => e.ID == ticket.EventId);
                            holder.SoldVolume += ticket.Amount;

                            if(ticket.Event == null)
                            {
                                ticket.Event = returnValue.Find(e => e.ID == ticket.EventId);
                            }
                        }
                    }
                    Sender.Tell(returnValue);
                }

                // Load all customer
                if(e == TransactionType.GetAllCustomer)
                {
                    var returnValue = database.Customers;

                    Sender.Tell(returnValue);
                }
            });

            // When Receiving an event -> Save new event to db 
            Receive<Event>(e =>
            {
                if (e.IsValid())
                {
                    e.SoldVolume = 0;
                    e.ID = Guid.NewGuid();
                    this.database.Events.Add(e);
                    this.RaiseEventDataChange(this.database.Events);
                }
            });

            // When Receiving an buy ticket Transaction 
            Receive<BuyTicketTransaction>(t =>
            {
                // Load ticket, correct customer and event
                var ticket = t.Ticket;
                var customer = this.database.Customers.Find(e => e.ID == t.CustomerId);
                var ev = this.database.Events.Find(e => e.ID == ticket.EventId);
                
                ticket.BoughtDate = DateTime.Now;
                
                // check if customer has enough balance to buy ticket and if there are enough tickets available
                if(customer.Budget >= ev.Price * ticket.Amount && ev.AvailableTickets >= ticket.Amount)
                {
                    // add ticket to customer
                    customer.Tickets.Add(ticket);
                    // calculate new budget
                    customer.Budget = customer.Budget - (ticket.Amount * ev.Price);
                    this.RaiseCustomerDataChange(customer);
                    this.RaiseEventDataChange(this.database.Events);
                    Sender.Tell(true);
                }
                else
                {
                    // tell there was a problem 
                    Sender.Tell(false); 
                }

            });
        }

        private void RaiseEventDataChange(IEnumerable<Event> NewData)
        {
            if (AdministrationActor.EventDataChange != null)
            {
                AdministrationActor.EventDataChange.Invoke(this, NewData);
            }
        }
        private void RaiseCustomerDataChange(Customer NewData)
        {
            if (AdministrationActor.CustomerDataChange != null)
            {
                AdministrationActor.CustomerDataChange.Invoke(this, NewData);
            }
        }
        public static Props Pros()
        {
            return Props.Create(() => new AdministrationActor());
        }
    }
}
