using Backend.Domain.Common;

namespace Backend.Domain.Entities.Auth
{
    public class User : Entity  
    {
        public string Username { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; }
        public Role Role { get; set; }

        public User(string email, string passwordHash, Role role, int id, string username)
        {
            Id = id;
            Email = !string.IsNullOrWhiteSpace(email) ? email : throw new ArgumentNullException(nameof(email));
            Username = !string.IsNullOrWhiteSpace(username) ? username : throw new ArgumentNullException(nameof(username));
            PasswordHash = !string.IsNullOrWhiteSpace(passwordHash) ? passwordHash : throw new ArgumentNullException(nameof(passwordHash));
            Role = role ?? throw new ArgumentNullException(nameof(role));
            

        }
    }
}
