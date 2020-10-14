namespace ComputerStore.Controllers.Request
{
    public class UpdateComputerDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageFilename { get; set; }
    }
}