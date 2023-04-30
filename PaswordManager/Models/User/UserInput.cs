namespace PasswordManager.Models.User
{
    public class UserInput
    {
        public Guid Id { get; set; }  
        public string Name { get; set; } = null!;

        public string Password { get; set; }

        public string? Photo { get; set; }

        public string EMail { get; set; }

        public string? Phone { get; set; }
    }
}
