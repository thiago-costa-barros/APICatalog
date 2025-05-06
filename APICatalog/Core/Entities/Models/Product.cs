using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using APICatalog.APICatalog.Core.Entities.Models.Base;

namespace APICatalog.APICatalog.Core.Entities.Models;

public class Product: AuditableEntity
{
    public int ProductId { get; set; }

    [Required]
    [StringLength(64, ErrorMessage = "The {0} must have at least {2} and at most {1} characters.", MinimumLength = 3)]
    public string? ProductName { get; set; }
    
    [StringLength(512, ErrorMessage = "The {0} must have at most {1} characters.")]
    public string? Description { get; set; }

    [Required]
    [Url]
    [StringLength(512, ErrorMessage = "The {0} must have at least {2} and at most {1} characters.", MinimumLength = 3)]
    public string? ImageUrl { get; set; }

    [Required]
    [Range(0.01, double.MaxValue)]
    [Column(TypeName="decimal(14,2)")]
    public decimal Price { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int Stock { get; set; } = 0;
 
    [ForeignKey("Category")]
    public int? CategoryId { get; set; }
    [JsonIgnore]
    public Category? Category { get; set; }
}
