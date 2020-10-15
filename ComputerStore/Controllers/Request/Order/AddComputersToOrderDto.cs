using System.Collections.Generic;

namespace ComputerStore.Controllers.Request.Order
{
    public class AddComputersToOrderDto
    {
        public List<AddOrderLineDto> OrderLines { get; set; }
    }
}