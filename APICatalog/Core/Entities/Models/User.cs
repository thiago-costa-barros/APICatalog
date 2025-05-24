using APICatalog.APICatalog.Core.Entities.Models.Base;
using Microsoft.EntityFrameworkCore;
using static APICatalog.Core.Common.Enum.PublicEnum;

namespace APICatalog.APICatalog.Core.Entities.Models;

[Index(nameof(Email), IsUnique = true)]
[Index(nameof(Login), IsUnique = true)]
public class User: AuditableEntity
{
    public int UserId { get; set; }
    
    public string? Login { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsAdmin { get; set; } = false;
    public UserType Type { get; set; } = UserType.TeamUser;

}
