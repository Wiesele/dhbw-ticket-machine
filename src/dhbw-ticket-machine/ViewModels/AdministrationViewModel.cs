using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
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

        private ObservableCollection<Event> _events;
        public ObservableCollection<Event> Events { get { return this._events; } set { SetProperty(ref _events, value); } }

        private ActorSystem ActorSystem {get; set; }

        private string _newName;
        public string NewName { get { return this._newName; } set { SetProperty(ref _newName, value); } }

        private string _newLocation;
        public string NewLocation { get { return this._newLocation; } set { SetProperty(ref _newLocation, value); } }

        private DateTime _newDate;
        public DateTime NewDate { get { return this._newDate; } set { SetProperty(ref _newDate, value); } }

        private float _newPrice;
        public float NewPrice { get { return this._newPrice; } set { SetProperty(ref _newPrice, value); } }


        private ObservableCollection<string> _suggestions;
        public ObservableCollection<string> Suggestions { get { return this._suggestions; } set { SetProperty(ref _suggestions, value); } }

        public AdministrationViewModel()
        {
            this.ActorSystem = ActorSystem.Create("Admin");
            this.Events = new ObservableCollection<Event>();
            this.Suggestions = new ObservableCollection<string>();
            this.ResetTextFields();
        }

        public void LoadSuggestions(string start)
        {
            var values = this.Events.GroupBy(e => e.Location).Where(e => e.Key.StartsWith(start)).Select(e => e.Key).OrderBy(e => e);

            var collection = new ObservableCollection<string>();
            collection.AddRange(values);
            this.Suggestions = collection;
        }

        public async void LoadData()
        {
            this.Events = new ObservableCollection<Event>();

            var actor = this.ActorSystem.ActorOf<AdministrationActor>();
            var task = actor.Ask(TransactionType.GetAll);

            var events = await task as IEnumerable<Event>;

            this.Events.AddRange(events);
            this.LoadSuggestions("");
        }

        public async void ResetTextFields()
        {
            this.NewPrice = 0;
            this.NewDate = DateTime.Now;
            this.NewLocation = "";
            this.NewName = "";
        }

        public async Task SaveNewEvent()
        {
            var newEvent = new Event()
            {
                Date = this.NewDate,
                Location = this.NewLocation,
                Name = this.NewName,
                Price = this.NewPrice,
                ID = Guid.NewGuid()
            };

            if (!newEvent.IsValid())
            {
                var msg = MessageBox.Show("Nicht alle Felder wurden gefüllt!", "Warnung!", MessageBoxButton.OK);
                return;
            }


            var actor = this.ActorSystem.ActorOf<AdministrationActor>();
            // Wait for Item to be inserted
            var task = actor.Ask(newEvent);
            await task;

            // Reload List
            this.LoadData();

            this.ResetTextFields();
        }
    }
}
