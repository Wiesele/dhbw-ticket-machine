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
            if(string.IsNullOrEmpty(ev.Location) && string.IsNullOrEmpty(ev.Name) && ev.Price > 0 && ev.Date != null )
            {
                return true;
            }
            return false;
        }
    }
}
