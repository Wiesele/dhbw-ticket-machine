using dhbw_ticket_machine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace dhbw_ticket_machine.Data
{
    public interface IDatabase
    {

        List<Customer> Customers { get; set; }
        List<Event> Events { get; set; }
    }
}
