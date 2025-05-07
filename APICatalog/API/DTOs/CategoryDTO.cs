using System.ComponentModel.DataAnnotations;

namespace APICatalog.API.DTOs
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }

        [Required]
        [StringLength(64, ErrorMessage = "The {0} must have at least {2} and at most {1} characters.", MinimumLength = 3)]
        public string? CategoryName { get; set; }

        [StringLength(512, ErrorMessage = "The {0} must have at most {1} characters.")]
        public string? Description { get; set; }

        [Required]
        [Url]
        [StringLength(512, ErrorMessage = "The {0} must have at least {2} and at most {1} characters.", MinimumLength = 3)]
        public string? ImageUrl { get; set; }
    }
}
