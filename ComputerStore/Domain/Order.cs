using System.Collections.Generic;

namespace ComputerStore
{
    public class Order
    {
        public string Guid { get; set; }
        public string Description { get; set; }
        public List<Computer> Computers { get; set; }
    }
}