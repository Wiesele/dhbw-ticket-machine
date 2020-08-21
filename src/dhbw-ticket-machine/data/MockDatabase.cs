using dhbw_ticket_machine.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace dhbw_ticket_machine.Data
{
    /// <summary>
    /// Mockdatabase with seed data
    /// </summary>
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
                    Price = 2.5f,
                    DaysBeforSalesStart = 5,
                    TotalTicketAmount = 150,
                    SaleStart = DateTime.Now - new TimeSpan(5,0,0,0),
                },
                new Event()
                {
                    Date = DateTime.Now,
                    ID = Guid.NewGuid(),
                    Location = "Gengenbach",
                    Name = "Hallo Welt ",
                    Price = 2.5f,
                    DaysBeforSalesStart = 5,
                    TotalTicketAmount = 150,
                    SaleStart = DateTime.Now - new TimeSpan(5,0,0,0),
                },
                new Event()
                {
                    Date = DateTime.Now,
                    ID = Guid.NewGuid(),
                    Location = "Stuttgart",
                    Name = "Hallo Welt ",
                    Price = 2.5f,
                    DaysBeforSalesStart = 5,
                    TotalTicketAmount = 150,
                    SaleStart = DateTime.Now - new TimeSpan(5,0,0,0),
                },
                new Event()
                {
                    Date = DateTime.Now,
                    ID = Guid.NewGuid(),
                    Location = "Offenburg",
                    Name = "Hallo Welt ",
                    Price = 2.5f,
                    DaysBeforSalesStart = 5,
                    TotalTicketAmount = 150,
                    SaleStart = DateTime.Now - new TimeSpan(5,0,0,0),
                },
                new Event()
                {
                    Date = DateTime.Now,
                    ID = testEventGuid,
                    Location = "Villingen Schwenningen",
                    Name = "Hallo Welt ",
                    Price = 2.5f,
                    DaysBeforSalesStart = 5,
                    TotalTicketAmount = 150,
                    SaleStart = DateTime.Now - new TimeSpan(5,0,0,0),
                },
            };

            var customers = new List<Customer>()
            {
                new Customer()
                {
                    ID = Guid.NewGuid(),
                    Adress = "Tennenbronn",
                    Name = "Felix Heß",
                    Budget = 1100,
                    AnualBudget = 1100,
                },
                new Customer()
                {
                    ID = Guid.NewGuid(),
                    Adress = "Bollenbach",
                    Name = "Marvin Rothmann",
                    AnualBudget = 1000,
                    Budget = 1000,
                },
                new Customer()
                {
                    ID = Guid.NewGuid(),
                    Adress = "Halbmeil",
                    Name = "Axel Schmidtke",
                    AnualBudget = 850,
                    Budget = 850,
                },
                new Customer()
                {
                    ID = Guid.NewGuid(),
                    Adress = "Schonach",
                    Name = "Julian Dold",
                    AnualBudget = 900,
                    Budget = 900,
                },
                new Customer()
                {
                    ID = Guid.NewGuid(),
                    Adress = "Freudenstadt",
                    Name = "Adrian Stengle",
                    AnualBudget = 750,
                    Budget = 750,
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
