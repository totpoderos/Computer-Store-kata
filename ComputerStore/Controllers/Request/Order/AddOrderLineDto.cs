namespace ComputerStore.Controllers.Request.Order
{
    public class AddOrderLineDto
    {
        public string ComputerId { get; set; }
        public int Quantity { get; set; }
    }
}