using System.ComponentModel.DataAnnotations;

namespace APICatalog.API.DTOs
{
    public class UserCreateDTO
    {
        [Required]
        [StringLength(128, MinimumLength = 8)]
        public string? Login { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 8)]
        public string? Password { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(256, MinimumLength = 10)]
        public string? Email { get; set; }

        public string? Phone { get; set; }
    }
}
