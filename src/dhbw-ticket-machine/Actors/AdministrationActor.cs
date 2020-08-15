using Akka.Actor;
using dhbw_ticket_machine.actors.TransactionTypes;
using dhbw_ticket_machine.Data;
using dhbw_ticket_machine.Extensions;
using dhbw_ticket_machine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace dhbw_ticket_machine.Actors
{
    public class AdministrationActor: ReceiveActor
    {
        IDatabase database;

        public delegate void EventDataChangeHandler(object source, IEnumerable<Event> Events);
        public static event EventDataChangeHandler EventDataChange;

        private void RaiseEventDataChange(IEnumerable<Event> NewData)
        {
            if(AdministrationActor.EventDataChange != null)
            {
                AdministrationActor.EventDataChange.Invoke(this, NewData);
            }
        }

        public AdministrationActor()
        {
            this.database = MockDatabase.Create();
            Receive<TransactionType>(e =>
            {
                if(e == TransactionType.GetAllEvents)
                {
                    var returnValue = database.Events;
                    foreach (var item in returnValue)
                    {
                        item.SoldVolume = 0;
                    }
                    foreach (var customer in database.Customers)
                    {
                        foreach (var ticket in customer.Tickets)
                        {
                            var holder = returnValue.Find(e => e.ID == ticket.EventId);
                            holder.SoldVolume += ticket.Amount;
                        }
                    }
                    Sender.Tell(returnValue);
                }

                if(e == TransactionType.GetAllCustomer)
                {
                    var returnValue = database.Customers;

                    Sender.Tell(returnValue);
                }
            });
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
        }

        public static Props Pros()
        {
            return Props.Create(() => new AdministrationActor());
        }
    }
}
