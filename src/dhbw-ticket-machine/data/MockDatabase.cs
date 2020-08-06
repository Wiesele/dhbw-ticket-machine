using dhbw_ticket_machine.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace dhbw_ticket_machine.data
{
    public class MockDatabase
    {
        /// <summary>
        /// Singelton Pattern
        /// </summary>
        private MockDatabase()
        {
            this.Customers = new List<Customer>();
            this.Events = new List<Event>();

            this.SeedDatabase();    
        }

        private void SeedDatabase()
        {
            // Add seed data here
            var lstEvents = new List<Event>()
            {
                new Event()
                {
                    Date = DateTime.Now,
                    ID = Guid.NewGuid(),
                    Location = "Haslach im Kinzigtal",
                    Name = "Hallo Welt ",
                    Price = 2.5f
                },
                new Event()
                {
                    Date = DateTime.Now,
                    ID = Guid.NewGuid(),
                    Location = "Gengenbach",
                    Name = "Hallo Welt ",
                    Price = 2.5f
                },
                new Event()
                {
                    Date = DateTime.Now,
                    ID = Guid.NewGuid(),
                    Location = "Stuttgard",
                    Name = "Hallo Welt ",
                    Price = 2.5f
                },
                new Event()
                {
                    Date = DateTime.Now,
                    ID = Guid.NewGuid(),
                    Location = "Offenburg",
                    Name = "Hallo Welt ",
                    Price = 2.5f
                },
                new Event()
                {
                    Date = DateTime.Now,
                    ID = Guid.NewGuid(),
                    Location = "Villingen Schwenningen",
                    Name = "Hallo Welt ",
                    Price = 2.5f
                },
            };

            this.Events.AddRange(lstEvents);
        }

        private MockDatabase Instance { get; set; }

        public MockDatabase GetInstance()
        {
            if(this.Instance == null)
            {
                this.Instance = new MockDatabase();
            }

            return this.Instance;
        }

        public List<Customer> Customers { get; set; }
        public List<Event> Events { get; set; }
    }
}
