using System.Collections.Generic;

namespace ComputerStore.Controllers.Request
{
    public class AddComputersToOrderDto
    {
        public List<AddOrderLineDto> OrderLines { get; set; }
    }
}