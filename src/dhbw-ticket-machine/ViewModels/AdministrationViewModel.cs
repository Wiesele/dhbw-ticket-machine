using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Transactions;
using Akka.Actor;
using dhbw_ticket_machine.actors.TransactionTypes;
using dhbw_ticket_machine.Actors;
using dhbw_ticket_machine.Data;
using dhbw_ticket_machine.Extensions;
using dhbw_ticket_machine.Models;

namespace dhbw_ticket_machine.ViewModels
{
    public class AdministrationViewModel : BindableBase
    {
        private Event _newEvent;
        public Event NewEvent { get { return this._newEvent; } set { SetProperty(ref _newEvent, value); } }

        private ObservableCollection<Event> _events;
        public ObservableCollection<Event> Events { get { return this._events; } set { SetProperty(ref _events, value); } }

        private ActorSystem ActorSystem {get; set; }

        public AdministrationViewModel()
        {
            this.ActorSystem = ActorSystem.Create("Admin");
            this.Events = new ObservableCollection<Event>();
        }

        public async void LoadData()
        {
            this.Events = new ObservableCollection<Event>();

            var actor = this.ActorSystem.ActorOf<AdministrationActor>();
            var task = actor.Ask(TransactionType.GetAll);

            var events = await task as IEnumerable<Event>;

            this.Events.AddRange(events);
        }
    }
}
