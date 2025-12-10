using System.Xml.Linq;
using WebApplicationAM.Domain.Common;
using WebApplicationAM.Domain.Interfaces.Providers;
using WebApplicationAM.Domain.Interfaces.Repositories;
using WebApplicationAM.Domain.Interfaces.Services;
using WebApplicationAM.Domain.Interfaces.Validators;

namespace WebApplicationAM.Domain.Features.Account
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository authRepository;
        private readonly ITokenProvider tokenProvider;
        private readonly IEmailValidator emailValidator;
        private readonly IPasswordValidator passwordValidator;
        private readonly IHashProvider hashProvider;
        private readonly ILogger<AuthService> logger;

        public AuthService(IAuthRepository authRepository, ITokenProvider tokenProvider, IEmailValidator emailValidator, IPasswordValidator passwordValidator, IHashProvider hashProvider, ILogger<AuthService> logger)
        {
            this.authRepository = authRepository ?? throw new ArgumentNullException(nameof(authRepository));
            this.tokenProvider = tokenProvider ?? throw new ArgumentNullException(nameof(tokenProvider));
            this.emailValidator = emailValidator ?? throw new ArgumentNullException(nameof(emailValidator));
            this.passwordValidator = passwordValidator ?? throw new ArgumentNullException(nameof(passwordValidator));
            this.hashProvider = hashProvider ?? throw new ArgumentNullException(nameof(hashProvider));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<OperationResult<string>> RegisterUser(string name, string email, string password)
        {
            
            throw new NotImplementedException();
        }
        public async Task<OperationResult<string>> Login( string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email)) 
                throw new ArgumentNullException(nameof(email));

            var user = await this.authRepository.GetUserByEmailAsync(email);
            if (user == null)
            {
                this.logger.LogInformation($"email {user} is null");
                return new OperationResult<string>.Error("Auth Error");
            }
            throw new NotImplementedException();
        }
        public async Task<OperationResult<string>> Logout(string name, string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
