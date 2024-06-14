using CoreAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreAPI.Database;

public sealed class CoreAPIDBContext(DbContextOptions<CoreAPIDBContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
}