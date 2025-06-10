using APICatalog.APICatalog.Core.Entities.Models;
using APICatalog.Core.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalog.APICataolog.Data.Context;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserToken> UserTokens { get; set; }
}