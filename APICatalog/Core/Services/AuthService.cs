using APICatalog.API.DTOs;
using APICatalog.APICatalog.Core.Entities.Models;
using APICatalog.Core.Services.Helpers;
using APICatalog.Core.Services.Interfaces;
using APICatalog.Data.Repositories.Interfaces;
using APICatalog.Core.Common.Enum;
using APICatalog.Core.Entities.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace APICatalog.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthServiceHelper _authServiceHelper;
        private readonly IAuthRepository _authRepository;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthService(IAuthServiceHelper authServiceHelper, IAuthRepository authRepository, IUserRepository userRepository, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _authServiceHelper = authServiceHelper;
            _authRepository = authRepository;
            _userRepository = userRepository;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TokenReponseDTO> GenerateAndSaveTokens(User user)
        {
            var tokens = _authServiceHelper.GenerateToken(user);
            if (string.IsNullOrEmpty(tokens.AccessToken) || string.IsNullOrEmpty(tokens.RefreshToken))
                throw new Exception("Failed to generate tokens.");

            if (!double.TryParse(_config["Jwt:RefreshTokenExpirationDays"], out var refreshDays))
                throw new Exception("Invalid configuration for RefreshTokenExpirationDays.");
            var refreshTokenExpirationDate = DateTime.UtcNow.AddDays(refreshDays);

            var hashedAccessToken = _authServiceHelper.HashToken(tokens.AccessToken);
            var hashedRefreshToken = _authServiceHelper.HashToken(tokens.RefreshToken);

            var accessIdentifier = tokens.Identifier;
            var refreshIdentifier = Guid.NewGuid();

            var dbAccessToken = await _authRepository.InsertTokenRepository(user.UserId, PublicEnum.TokenType.AccessToken, hashedAccessToken, tokens.ExpirationDate, accessIdentifier);
            var dbRefreshToken = await _authRepository.InsertTokenRepository(user.UserId, PublicEnum.TokenType.RefreshToken, hashedRefreshToken, refreshTokenExpirationDate, refreshIdentifier);

            return tokens;
        }

        public async Task<TokenReponseDTO> LoginService(AuthLoginRequestDTO loginRequestDTO)
        {
            if (string.IsNullOrEmpty(loginRequestDTO.Email))
                throw new ArgumentNullException(nameof(loginRequestDTO), "Email cannot be null or empty.");

            if (string.IsNullOrEmpty(loginRequestDTO.Password))
                throw new ArgumentNullException(nameof(loginRequestDTO), "Password cannot be null or empty.");

            User? user = await _userRepository.GetUserByEmailRepository(loginRequestDTO.Email);
            if (user is null || string.IsNullOrEmpty(user.Password) || !_authServiceHelper.VerifyPasswordHasher(loginRequestDTO.Password, user.Password))
                throw new UnauthorizedAccessException("Invalid username or password.");

            await RevokeAllTokensByUserIdService(user.UserId);

            return await GenerateAndSaveTokens(user);
        }

        public Task<bool> LogoutService(int userId, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<TokenReponseDTO> RefreshTokenService(AuthRefreshRequestDTO refreshTokenDTO)
        {
            if(string.IsNullOrEmpty(refreshTokenDTO.RefreshToken))
                throw new ArgumentNullException(nameof(refreshTokenDTO), "Refresh token cannot be null or empty.");


            UserToken userToken = await _authRepository.GetTokenByIdentifierRepository(refreshTokenDTO.Identifier, PublicEnum.TokenType.RefreshToken);

            await RevokeAllTokensByUserIdService(userToken.UserId);

            User? user = await _userRepository.GetUserByIdRepository(userToken.UserId);
            if(user == null)
                throw new InvalidOperationException("User not found.");

            return await GenerateAndSaveTokens(user);
        }

        public async Task RevokeAllTokensByUserIdService(int userId)
        {
            var existingTokens = await _authRepository.GetTokensByUserIdRepository(userId);

            foreach (var token in existingTokens)
            {
                await _authRepository.RevokeUserTokenRepository(userId, token.Identifier, token.Type);
            }
        }

        public async Task RevokeTokenByUserIdAndTokenTypeService(int userId, Guid identifier, PublicEnum.TokenType type)
        {
            var userToken = await _authRepository.GetTokenByIdentifierRepository(identifier, type);
            if (userToken == null || userToken.UserId != userId)
                return;
            await _authRepository.RevokeUserTokenRepository(userId, identifier, type);
        }

        public async Task<UserToken> ValidateAccessTokenService(string token)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
            var jtiClaim = _httpContextAccessor.HttpContext?.User.FindFirst(JwtRegisteredClaimNames.Jti);

            if (userIdClaim == null || jtiClaim == null)
                throw new UnauthorizedAccessException("Invalid token claims.");

            int userId = int.Parse(userIdClaim.Value);
            Guid identifier = Guid.Parse(jtiClaim.Value);

            UserToken? userToken = await _authRepository.GetTokenByIdentifierRepository(identifier, PublicEnum.TokenType.AccessToken);
            if (userToken == null || userToken.UserId != userId)
                throw new UnauthorizedAccessException("Invalid token.");

            if (userToken.ExpirationDate < DateTime.UtcNow)
            {
                await RevokeTokenByUserIdAndTokenTypeService(userToken.UserId, userToken.Identifier, PublicEnum.TokenType.AccessToken);
                throw new UnauthorizedAccessException("Expired token.");
            }

            var isValid = _authServiceHelper.VerifyTokenHasher(token, userToken.JwtToken);
            if (!isValid)
                throw new UnauthorizedAccessException("Token hash mismatch.");

            return userToken;
        }
    }
}
