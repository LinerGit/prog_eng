using Backend.Domain.Entities.Auth;
using Backend.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend.Presentation.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        private readonly IHashProvider _hashProvider;

        public AuthService(IUserRepository userRepository, IConfiguration config, IHashProvider hashProvider)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _hashProvider = hashProvider ?? throw new ArgumentNullException(nameof(hashProvider));
        }

        public async Task<string> RegisterAsync(string username, string email, string password, string roleName)
        {
            // 1. Проверка уникальности
            if (await _userRepository.GetByEmailAsync(email) != null)
                throw new Exception("Email уже занят");

            // 2. Поиск роли
            var role = await _userRepository.GetRoleByNameAsync(roleName)
                       ?? throw new Exception("Указанная роль не существует");

            // 3. Хеширование пароля
            string passwordHash = _hashProvider.HashPassword(password);

            // 4. Создание пользователя
            var user = new User(email, passwordHash, role, 0, username);
            await _userRepository.AddAsync(user);

            return GenerateToken(user);
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null || !_hashProvider.VerifyPassword(password, user.PasswordHash))
                throw new Exception("Неверный email или пароль");

            return GenerateToken(user);
        }

        private string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.Name) // Роль для атрибута [Authorize]
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
