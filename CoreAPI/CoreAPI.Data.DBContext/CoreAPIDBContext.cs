using CoreAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreAPI.Data.DBContext;

public sealed class CoreAPIDBContext(DbContextOptions<CoreAPIDBContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
}