using APICatalog.API.DTOs;
using APICatalog.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace APICatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthLoginRequestDTO loginRequestDTO)
        {
            if (loginRequestDTO == null) return BadRequest("Invalid login request.");

            TokenReponseDTO token = await _authService.LoginService(loginRequestDTO);

            if (token == null) return Unauthorized("Invalid credentials.");

            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] AuthRefreshRequestDTO refreshTokenDTO)
        {
            if (string.IsNullOrEmpty(refreshTokenDTO.RefreshToken)) return BadRequest("Invalid token.");

            TokenReponseDTO newToken = await _authService.RefreshTokenService(refreshTokenDTO);

            if (newToken == null) return Unauthorized("Invalid token.");

            return Ok(newToken);
        }
    }
}
