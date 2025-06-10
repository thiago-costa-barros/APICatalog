using APICatalog.Core.Services.Interfaces;
using APICatalog.Data.Repositories.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using static APICatalog.Core.Common.Enum.PublicEnum;

namespace APICatalog.API.Middlewares
{
    public class TokenRevocationMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenRevocationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IAuthRepository authRepository)
        {
            var user = context.User;

            if (user.Identity?.IsAuthenticated == true)
            {
                var jti = user.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;

                if (Guid.TryParse(jti, out var accessIdentifier))
                {
                    var isValidAccessToken = await authRepository.GetTokenByIdentifierRepository(accessIdentifier, TokenType.AccessToken);
                    if (isValidAccessToken == null)
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync("Access token has been revoked.");
                        return;
                    }
                }
            }

            await _next(context);
        }
    }
}
