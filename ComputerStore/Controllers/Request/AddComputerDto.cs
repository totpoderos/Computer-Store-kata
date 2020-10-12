namespace ComputerStore.Controllers.Request
{
    public class AddComputerDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Id { get; set; }
        public string ImageFilename { get; set; }
    }
}