namespace ComputerStore.Controllers.Response
{
    public class UserInfoDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsRoot { get; set; }
    }
}