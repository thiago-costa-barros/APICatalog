using System.ComponentModel.DataAnnotations;

namespace APICatalog.API.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }

        [Required]
        [StringLength(64, ErrorMessage = "The {0} must have at least {2} and at most {1} characters.", MinimumLength = 3)]
        public string? ProductName { get; set; }

        [StringLength(512, ErrorMessage = "The {0} must have at most {1} characters.")]
        public string? Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Stock { get; set; } = 0;

        [Required]
        [Url]
        [StringLength(512, ErrorMessage = "The {0} must have at least {2} and at most {1} characters.", MinimumLength = 3)]
        public string? ImageUrl { get; set; }

        public int CategoryId { get; set; }
    }
}
