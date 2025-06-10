using System.ComponentModel.DataAnnotations;

namespace APICatalog.API.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string? Login { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
