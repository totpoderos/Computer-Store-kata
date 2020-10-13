using System.Collections.Generic;

namespace ComputerStore.Controllers.Request
{
    public class NewOrderDto
    {
        public List<NewOrderLineDto> OrderLines { get; set; }
    }
}