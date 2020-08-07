using System;
using System.Collections.Generic;
using System.Text;

namespace dhbw_ticket_machine.Models
{
    public class Ticket: Event
    {
        public DateTime BoughtDate { get; set; }
    }
}
