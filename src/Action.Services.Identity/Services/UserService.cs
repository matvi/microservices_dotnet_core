using System.Threading.Tasks;
using Action.Common.Auth;
using Action.Common.Exceptions;
using Action.Services.Identity.Domain.Models;
using Action.Services.Identity.Domain.Repositories;
using Action.Services.Identity.Domain.Services;

namespace Action.Services.Identity.Services {
    public class UserService : IUserService {
        private readonly IEncrypter _encrypter;
        private readonly IJwtHandler _jwtHandler;
        private readonly IUserRepository _userRepository;
        public UserService (IUserRepository userRepository, IEncrypter encrypter, IJwtHandler jwtHandler) 
        {
            _userRepository = userRepository;
            _encrypter = encrypter;
            _jwtHandler = jwtHandler;
        }

        public async Task<JsonWebToken> LoginAsync (string email, string password) 
        {
            var user = await _userRepository.GetAsync(email);
            if( user == null)
            {
                throw new ActioException("User_not_exist", $"The user with this email : {email} does not exist");
            }
            
            if (!user.ValidatePassword(password, _encrypter) )
            {
                throw new ActioException("Invalid_password", "Password not valid");
            }
            
            return _jwtHandler.GenerateToken(user.Id);
        }

        public async Task RegisterAsync (string email, string password, string name)
        {
            var user = await _userRepository.GetAsync(email);
            if (user != null)
            {
                throw new ActioException("EMAIL_IN_USE", $"Email: {email} already in use");
            }
            user = new User(email,name);
            user.SetPassword(password, _encrypter);
            await _userRepository.AddAsync(user);
        }
    }
}