using Backend.Domain.Common;

namespace Backend.Infrastructure.Auth
{
    public class JwtCurrentUser : ICurrentUser
    {
        public int UserId { get; }
        public string Email { get; }
        public string Role { get;}

        public JwtCurrentUser(int userId, string email, string role)
        {
            UserId = userId;
            Email = email;
            Role = role;
        }
    }
}
