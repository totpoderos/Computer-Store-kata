using System;
using System.Collections.Generic;

namespace ComputerStore.Domain
{
    public class Order
    {
        public string Guid { get; set; }
        public List<Computer> Computers { get; set; }
        public DateTime Date { get; set; }
    }
}