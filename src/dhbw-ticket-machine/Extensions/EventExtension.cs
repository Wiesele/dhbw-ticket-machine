using dhbw_ticket_machine.Models;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace dhbw_ticket_machine.Extensions
{
    public static class EventExtension
    {
        public static bool IsValid(this Event ev)
        {
            if(!string.IsNullOrEmpty(ev.Location) && !string.IsNullOrEmpty(ev.Name) && ev.Price > 0 && ev.Date != null && ev.TotalTicketAmount > 0 && ev.DaysBeforSalesStart > 0 )
            {
                return true;
            }
            return false;
        }

        public static bool IsCurrentlySelling(this Event ev)
        {
            var now = DateTime.Now.Date;

            var value =  ev.SaleStart.Date <= now && ev.SaleEnd.Date >= now;
            return value;
        }
    }
}
