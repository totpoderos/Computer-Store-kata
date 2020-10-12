using System.Collections.Generic;

namespace ComputerStore.Controllers.Request
{
    public class AddComputersToOrderDto
    {
        public List<string> ComputerId { get; set; }
    }
}