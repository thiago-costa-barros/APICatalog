using APICatalog.APICatalog.Core.Entities.Models.Base;

namespace APICatalog.APICatalog.Core.Entities.Models;

public class User: AuditableEntity
{
    public int UserId { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsStaff { get; set; } = false;
    public bool IsAdmin { get; set; } = false;
}
