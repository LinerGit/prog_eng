using Backend.Domain.Entities.Auth;
using Backend.Domain.Interfaces;
using Backend.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context; // Ваш контекст БД

        public UserRepository(AppDbContext context) { _context = context ?? throw new ArgumentNullException(nameof(context)); }

        public async Task<User?> GetByEmailAsync(string email)
            => await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == email);

        public async Task<User?> GetByUsernameAsync(string username)
            => await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Username == username);

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<Role?> GetRoleByNameAsync(string roleName)
            => await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
    }
}
