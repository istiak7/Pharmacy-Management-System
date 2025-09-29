namespace Pharmacy_Management_System.Application.Dtos.Requests.Users
{
    public class UserRequest
    {
        private string _Password { get; set; } = null!;
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password
        {
            get => _Password;
            set => _Password = BCrypt.Net.BCrypt.HashPassword(value);
        }
    }
}
