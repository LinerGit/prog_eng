using Backend.Domain.Entities.Auth;

namespace Backend.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByUsernameAsync(string username);
        Task AddAsync(User user);
        Task<Role?> GetRoleByNameAsync(string roleName);
    }
}
