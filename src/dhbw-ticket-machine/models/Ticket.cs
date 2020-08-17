using dhbw_ticket_machine.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace dhbw_ticket_machine.Models
{
    public class Ticket: Entity
    {
        public Guid EventId { get; set; }
        public int Amount { get; set; }
        public DateTime BoughtDate { get; set; }
        public Event Event { get; set; }
    }
}
