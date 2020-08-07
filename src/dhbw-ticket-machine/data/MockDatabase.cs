using dhbw_ticket_machine.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace dhbw_ticket_machine.Data
{
    public class MockDatabase: IDatabase
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
            var testEventGuid = Guid.NewGuid();
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
                    ID = testEventGuid,
                    Location = "Villingen Schwenningen",
                    Name = "Hallo Welt ",
                    Price = 2.5f
                },
            };

            var customers = new List<Customer>()
            {
                new Customer()
                {
                    ID = Guid.NewGuid(),
                    Adress = "TestAdresse",
                    Name = "Felix Heß",
                    Tickets = new List<Ticket>()
                    {
                        new Ticket()
                        {
                            Amount = 5,
                            EventId = testEventGuid,
                            BoughtDate = DateTime.Now,
                            ID = Guid.NewGuid()
                        }
                    }
                }
            };

            this.Events.AddRange(lstEvents);
            this.Customers.AddRange(customers);
        }

        private static MockDatabase Instance { get; set; }

        public static MockDatabase Create()
        {
            if(MockDatabase.Instance == null)
            {
                MockDatabase.Instance = new MockDatabase();
            }

            return MockDatabase.Instance;
        }

        public List<Customer> Customers { get; set; }
        public List<Event> Events { get; set; }
    }
}
