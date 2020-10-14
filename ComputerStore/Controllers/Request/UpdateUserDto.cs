namespace ComputerStore.Controllers.Request
{
    public class UpdateUserDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsRoot { get; set; }
    }
}