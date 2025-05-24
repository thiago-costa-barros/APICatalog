using APICatalog.API.DTOs;
using APICatalog.APICataolog.Data.Context;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace APICatalog.Data.Repositories.DAOs
{
    public class AuthDAO
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public AuthDAO(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _configuration = config;
        }
        public async Task<TokenReponseDTO?> AuthenticateAsync(LoginRequestDTO loginRequest)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Login == loginRequest.Login);

            if (user == null)
                return null;

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password);
            if (!isPasswordValid)
                return null;

            var secretKey = _configuration["Jwt:SecretKey"];
            if (string.IsNullOrEmpty(secretKey))
                throw new InvalidOperationException("JWT SecretKey is not configured.");

            var expirationMinutesConfig = _configuration["Jwt:ExpirationMinutes"];
            if (string.IsNullOrEmpty(expirationMinutesConfig))
                throw new InvalidOperationException("JWT ExpirationMinutes is not configured.");

            var expirationMinutes = int.Parse(expirationMinutesConfig);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
            };

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddMinutes(expirationMinutes),
                signingCredentials: creds,
                claims: claims
            );

            return new TokenReponseDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpirationDate = DateTime.Now.AddMinutes(expirationMinutes)
            };

            return null;
        }
        public async Task<bool> ValidateTokenAsync(string token)
        {
            // Implement your logic to validate the token
            return await Task.FromResult(true);
        }
    }
}
