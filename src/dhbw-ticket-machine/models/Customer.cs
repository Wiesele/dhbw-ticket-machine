using System;
using System.Collections.Generic;
using System.Text;

namespace dhbw_ticket_machine.Models
{
    public class Customer: Entity
    {
        public Customer()
        {
            this.Tickets = new List<Ticket>();
        }

        public string Name { get; set; }
        public string Adress { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
