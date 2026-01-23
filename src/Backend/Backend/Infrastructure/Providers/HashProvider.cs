using Backend.Domain.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Backend.Infrastructure.Providers
{
    public class HashProvider : IHashProvider
    {
        public string HashPassword(string password)
        {
            if(password == null)
                throw new ArgumentNullException(nameof(password));
            using SHA256 sha256Hash = SHA256.Create();
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }

        public bool VerifyPassword(string providedPassword, string storedHash)
        {
            if(providedPassword == null)
                throw new ArgumentNullException(nameof(providedPassword));
            if(storedHash == null)
            {
                throw new ArgumentNullException(nameof(storedHash));
            }
                var hashedProvidedPassword = HashPassword(providedPassword);
            return hashedProvidedPassword == storedHash;
        }
    }
}
