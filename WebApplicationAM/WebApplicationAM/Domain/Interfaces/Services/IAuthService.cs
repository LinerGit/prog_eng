using WebApplicationAM.Domain.Common;

namespace WebApplicationAM.Domain.Interfaces.Services;



public interface IAuthService
{
    Task<OperationResult<string>> RegisterUser(string name, string email, string password);

    Task<OperationResult<string>> Login(string email, string password);

    Task<OperationResult<string>> Logout(string name, string email, string password);
}
