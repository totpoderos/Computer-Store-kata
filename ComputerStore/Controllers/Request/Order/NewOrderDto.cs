using System.Collections.Generic;

namespace ComputerStore.Controllers.Request.Order
{
    public class NewOrderDto
    {
        public List<NewOrderLineDto> OrderLines { get; set; }
    }
}