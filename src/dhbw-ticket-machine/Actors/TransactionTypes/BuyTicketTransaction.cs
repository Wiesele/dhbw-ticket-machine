using dhbw_ticket_machine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace dhbw_ticket_machine.Actors.TransactionTypes
{
    public class BuyTicketTransaction
    {
        public Ticket Ticket { get; set; }
        public Guid CustomerId { get; set; }
    }
}
