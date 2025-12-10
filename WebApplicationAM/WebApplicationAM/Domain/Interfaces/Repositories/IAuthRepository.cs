using WebApplicationAM.Domain.Model;

namespace WebApplicationAM.Domain.Interfaces.Repositories
{
    public interface IAuthRepository
    {
        Task AddUserAsync(User user);

        Task UpdateUserAsync(User user, Guid id);

        Task<User> GetUserByEmailAsync(string email);

        Task<User> GetUserAsync(Guid id);
    }
}
