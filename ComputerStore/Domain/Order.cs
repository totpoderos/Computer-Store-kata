using System;
using System.Collections.Generic;

namespace ComputerStore.Domain
{
    public class Order
    {
        public string Guid { get; set; }
        public List<OrderLine> OrderLines { get; set; }
        public DateTime Date { get; set; }
    }
}