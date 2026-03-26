using System.Text;

namespace Backend.Domain.Interfaces
{
    public interface IHashProvider
    {
        public string HashPassword(string password);
        public bool VerifyPassword(string providedPassword, string storedHash);
        
    }
}
