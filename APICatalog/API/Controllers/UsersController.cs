using APICatalog.API.DTOs;
using APICatalog.API.DTOs.Mapping;
using APICatalog.APICatalog.Core.Entities.Models;
using APICatalog.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserCreateDTO userCreateDTO)
        {
            if (userCreateDTO == null) throw new ArgumentNullException(nameof(userCreateDTO), "User registration data is null.");

            var userEntity = userCreateDTO.MapToUser();
            if (userEntity == null) throw new ArgumentNullException(nameof(userEntity), "User entity is null.");

            User? userRegistered = await _userService.RegisterUserService(userEntity);

            if (userRegistered == null)
                return BadRequest("User registration failed.");

            UserDTO userResponseDTO = userRegistered.MapToUserDTO();

            return Ok(userResponseDTO);
        }
    }
}
