using System;
using System.Collections.Generic;
using System.Text;

namespace dhbw_ticket_machine.models
{
    public class Event: Entity
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public float Price { get; set; }
    }
}
