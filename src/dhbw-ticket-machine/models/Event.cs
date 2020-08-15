using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace dhbw_ticket_machine.Models
{
    public class Event: Entity
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateSaleStart { get; set; }
        public DateTime SaleEnd { get
            {
                if(this.Date != null)
                {
                    var Timespan = new TimeSpan(this.DaysBeforSalesStart,0,0,0);
                    return DateSaleStart + Timespan;
                }
                return DateTime.Now;
            }
        }
        public string Location { get; set; }
        public float Price { get; set; }
        public int SoldVolume { get; set; }
        public int DaysBeforSalesStart { get; set; }
        public int TotalTicketAmount { get; set; }

        public int AvailableTickets { get { return TotalTicketAmount - SoldVolume; } }
    }
}
