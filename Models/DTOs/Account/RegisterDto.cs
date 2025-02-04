namespace MoneyKeeper.Models.DTOs.Account
{
    public class RegisterDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
