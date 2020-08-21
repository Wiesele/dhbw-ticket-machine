using dhbw_ticket_machine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace dhbw_ticket_machine.Actors.TransactionTypes
{
    /// <summary>
    /// Transaction for buying tickets
    /// </summary>
    public class BuyTicketTransaction
    {
        public Ticket Ticket { get; set; }
        public Guid CustomerId { get; set; }
    }
}
