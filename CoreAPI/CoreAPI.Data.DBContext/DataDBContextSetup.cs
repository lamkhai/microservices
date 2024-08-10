using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CoreAPI.Data.DBContext;

public static class DataDBContextSetup
{
    public static void AddDbContext(IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<CoreAPIDBContext>(options =>
                options.UseNpgsql(connectionString,
                                  b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name)
            )
        );
    }
}