using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using APICatalog.APICatalog.Core.Entities.Models.Base;

namespace APICatalog.APICatalog.Core.Entities.Models;

public class Category: AuditableEntity
{
    public Category()
    {
        Products = new Collection<Product>();
    }
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
    public ICollection<Product>? Products { get; set; }

}
