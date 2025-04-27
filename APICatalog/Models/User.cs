namespace APICatalog.Models;

public class User
{
    public int UserId { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public bool IsActive { get; set; }
    public bool IsStaff { get; set; }
    public bool IsAdmin { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public DateTime? DeletionDate { get; set; }
    public int? CreationUserId { get; set; }
    public int? UpdateUserId { get; set; }
}
