using APICatalog.API.DTOs;
using APICatalog.APICatalog.Core.Entities.Models;
using APICatalog.Core.Services.Helpers;
using APICatalog.Core.Services.Interfaces;
using APICatalog.Data.Repositories.Interfaces;
using APICatalog.Core.Common.Enum;

namespace APICatalog.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly ITokenServiceHelper _tokenServiceHelper;
        private readonly IAuthRepository _authRepository;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        public AuthService(ITokenServiceHelper tokenServiceHelper, IAuthRepository authRepository, IUserRepository userRepository, IConfiguration config)
        {
            _tokenServiceHelper = tokenServiceHelper;
            _authRepository = authRepository;
            _userRepository = userRepository;
            _config = config;
        }
        public async Task<TokenReponseDTO> LoginService(LoginRequestDTO loginRequestDTO)
        {
            if (string.IsNullOrEmpty(loginRequestDTO.Email))
                throw new ArgumentNullException(nameof(loginRequestDTO), "Email cannot be null or empty.");

            if (string.IsNullOrEmpty(loginRequestDTO.Password))
                throw new ArgumentNullException(nameof(loginRequestDTO), "Password cannot be null or empty.");

            User? user = await _userRepository.GetUserByEmailRepository(loginRequestDTO.Email);
            if (user is null || string.IsNullOrEmpty(user.Password) || !_tokenServiceHelper.VerifyPasswordHaser(user.Password, loginRequestDTO.Password))
                throw new UnauthorizedAccessException("Invalid username or password.");

            await _authRepository.RevokeAllTokensByUserIdRepository(user.UserId);

            var tokens = _tokenServiceHelper.GenerateToken(user);
            if(string.IsNullOrEmpty(tokens.AccessToken) || string.IsNullOrEmpty(tokens.RefreshToken))
                throw new Exception("Failed to generate tokens.");

            var refreshTokenExpirationDate = DateTime.UtcNow.AddDays(Convert.ToDouble(_config["Jwt:RefreshTokenExpirationDays"]));

            var dbAccessToken = await _authRepository.InsertTokenRepository(user.UserId, PublicEnum.TokenType.AccessToken, tokens.AccessToken, tokens.ExpirationDate);
            var dbRefreshToken = await _authRepository.InsertTokenRepository(user.UserId, PublicEnum.TokenType.RefreshToken, tokens.RefreshToken, refreshTokenExpirationDate);

            return tokens;
        }

        public Task<bool> LogoutService(int userId, string token)
        {
            throw new NotImplementedException();
        }

        public Task<TokenReponseDTO> RefreshTokenService(string token)
        {
            throw new NotImplementedException();
        }

        public Task<TokenValidationResultDTO> ValidateTokenService(string token)
        {
            throw new NotImplementedException();
        }
    }
}
