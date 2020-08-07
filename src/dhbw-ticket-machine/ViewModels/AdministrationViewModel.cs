using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Text;
using dhbw_ticket_machine.Data;
using dhbw_ticket_machine.Extensions;
using dhbw_ticket_machine.Models;

namespace dhbw_ticket_machine.ViewModels
{
    public class AdministrationViewModel:BindableBase
    {
        private ObservableCollection<Event> _events;
        public ObservableCollection<Event> Events { get { return this._events; } set { SetProperty(ref _events, value); } }

        public AdministrationViewModel()
        {
            this.Events = new ObservableCollection<Event>();
            //this.Events. ser.Events;
        }

        public void LoadData()
        {
            var ser = MockDatabase.Create();
            this.Events.AddRange(ser.Events);
        }
    }
}
