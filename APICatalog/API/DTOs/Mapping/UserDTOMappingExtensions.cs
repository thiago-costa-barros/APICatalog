using APICatalog.APICatalog.Core.Entities.Models;

namespace APICatalog.API.DTOs.Mapping
{
    public static class UserDTOMappingExtensions
    {
        public static User MapToUser( this UserCreateDTO userCreateDTO)
        {
            if (userCreateDTO == null) throw new ArgumentNullException(nameof(userCreateDTO));

            return new User
            {
                Login = userCreateDTO.Login,
                Password = BCrypt.Net.BCrypt.HashPassword(userCreateDTO.Password),
                Email = userCreateDTO.Email,
                Phone = userCreateDTO.Phone

            };
        }
        public static UserDTO MapToUserDTO(this User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            return new UserDTO
            {
                UserId = user.UserId,
                Login = user.Login,
                Email = user.Email,
                Phone = user.Phone
            };
        }
    }
}
