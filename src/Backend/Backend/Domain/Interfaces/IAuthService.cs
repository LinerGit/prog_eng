using Backend.Domain.Entities.Auth;
using Backend.Infrastructure.Persistence.Repositories;
using Backend.Infrastructure.Providers;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend.Domain.Interfaces
{
    public interface IAuthService
    {
        public Task<string> RegisterAsync(string username, string email, string password, string roleName);


        public Task<string> LoginAsync(string email, string password);

        
    }
}
