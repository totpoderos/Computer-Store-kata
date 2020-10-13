using System;
using System.Collections.Generic;

namespace ComputerStore.Controllers.Response
{
    public class OrderInformationDto
    {
        public string OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderLineInfoDto> OrderLinesInfo { get; set; }
        public Decimal Price { get; set; }
    }
}