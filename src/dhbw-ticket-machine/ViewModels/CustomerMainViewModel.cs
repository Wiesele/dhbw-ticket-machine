using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using dhbw_ticket_machine.actors.TransactionTypes;
using dhbw_ticket_machine.Actors;
using dhbw_ticket_machine.Extensions;
using dhbw_ticket_machine.Models;

namespace dhbw_ticket_machine.ViewModels
{
    public class CustomerMainViewModel: BindableBase
    {
        private ObservableCollection<Event> _events;
        public ObservableCollection<Event> Events { get { return this._events; } set { SetProperty(ref _events, value); } }

        public CustomerMainViewModel()
        {
            this.Events = new ObservableCollection<Event>();
            this.ResetTextFields();

            AdministrationActor.EventDataChange += EventDataChange;
        }

        private void EventDataChange(object sender, IEnumerable<Event> events)
        {
            this.LoadData();
        }


        public async void LoadData()
        {
            this.Events = new ObservableCollection<Event>();

            var actor = MainWindow.ActorSystem.ActorOf<AdministrationActor>();
            var task = actor.Ask(TransactionType.GetAll);

            var events = await task as IEnumerable<Event>;

            this.Events.AddRange(events);
        }

        public async void ResetTextFields()
        {
        }

        public async Task BuyTickets()
        {
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


            //var actor = MainWindow.ActorSystem.ActorOf<AdministrationActor>();
            //// Wait for Item to be inserted
            //var task = actor.Ask(newEvent);
            //await task;

            //// Reload List
            //this.LoadData();

            //this.ResetTextFields();
        }
    }
}
