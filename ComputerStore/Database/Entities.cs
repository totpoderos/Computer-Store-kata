using System;
using System.Collections.Generic;
using System.Linq;

namespace ComputerStore
{
    public class Entities
    {
        public List<Computer> Computers = new List<Computer>
        {
            new Computer()
            {
                Guid = Guid.NewGuid().ToString(), Name = "MacBook Pro 15", Description = "Macbook pro 15' Description",
                Price = 1500, ImageFilename = "MBP15.png"
            },
            new Computer()
            {
                Guid = Guid.NewGuid().ToString(), Name = "MacBook Pro 13", Description = "Macbook pro 13' Description",
                Price = 1300, ImageFilename = "MBP13.png"
            },
            new Computer()
            {
                Guid = Guid.NewGuid().ToString(), Name = "MacBook Air 13", Description = "Macbook pro 13' Description",
                Price = 1400, ImageFilename = "MBA13.png"
            },
            new Computer()
            {
                Guid = Guid.NewGuid().ToString(), Name = "iMac 21", Description = "iMac 21' Description", Price = 1800,
                ImageFilename = "iMac21.png"
            },
            new Computer()
            {
                Guid = Guid.NewGuid().ToString(), Name = "iMac 27", Description = "iMac 27' Description", Price = 2100,
                ImageFilename = "iMac27.png"
            },
        }; 
        
        public List<Order> Orders = new List<Order>();

        private List<Computer> _computers;
        private List<Order> _orders;

        public Entities()
        {
            _computers = MakeACopy(Computers);
            _orders = MakeACopy(Orders);
        }

        private List<T> MakeACopy<T>(List<T> source)
        {
            var copy = new T[source.Count];
            source.CopyTo(copy);
            return copy.ToList();
        }

        public void Save()
        {
            _computers = MakeACopy(Computers);
            _orders = MakeACopy(Orders);
        }
        
        public void Undo()
        {
            Computers = MakeACopy(_computers);
            Orders = MakeACopy(_orders);
        }
    }
}