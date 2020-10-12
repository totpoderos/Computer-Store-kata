namespace ComputerStore.Domain
{
    public class User
    {
        public string Guid { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public byte[] Password { get; set; }
        public string Email { get; set; }
        public bool IsRoot { get; set; }
    }
}