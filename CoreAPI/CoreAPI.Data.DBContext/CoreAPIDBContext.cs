using CoreAPI.Data.Core.Services;
using CoreAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreAPI.Data.DBContext;

public sealed class CoreAPIDBContext : DbContext
{
    private readonly IConstantSQLService _constantSQLService;

    public CoreAPIDBContext(
        IConstantSQLService constantSQLService,
        DbContextOptions<CoreAPIDBContext> options) : base(options)
    {
        _constantSQLService = constantSQLService;
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (string extensionName in _constantSQLService.ExtensionNames)
        {
            modelBuilder.HasPostgresExtension(extensionName);
        }

        modelBuilder.Entity<Role>()
                    .Property(x => x.Id)
                    .HasDefaultValueSql(_constantSQLService.DefaultGuid);
        modelBuilder.Entity<Role>()
                    .Property(x => x.CreatedDate)
                    .HasDefaultValueSql(_constantSQLService.DefaultUTCDateTime);

        modelBuilder.Entity<User>()
                    .Property(x => x.Id)
                    .HasDefaultValueSql(_constantSQLService.DefaultGuid);
        modelBuilder.Entity<User>()
                    .Property(x => x.CreatedDate)
                    .HasDefaultValueSql(_constantSQLService.DefaultUTCDateTime);
        modelBuilder.Entity<User>()
                    .HasIndex(x => x.UserName)
                    .IsUnique();

        modelBuilder.Entity<UserRole>()
                    .HasKey(x => new { x.RoleId, x.UserId });
        modelBuilder.Entity<UserRole>()
                    .Property(x => x.CreatedDate)
                    .HasDefaultValueSql(_constantSQLService.DefaultUTCDateTime);

        base.OnModelCreating(modelBuilder);
    }
}