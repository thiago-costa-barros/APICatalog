﻿using APICatalog.Models.Base;

namespace APICatalog.Models;

public class User: BaseEntity
{
    public int UserId { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public bool IsActive { get; set; }
    public bool IsStaff { get; set; }
    public bool IsAdmin { get; set; }
}
