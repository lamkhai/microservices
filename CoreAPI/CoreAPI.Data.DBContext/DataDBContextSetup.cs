using CoreAPI.Data.Core.Services;
using CoreAPI.Data.DBContext.Services;
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

    public static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IConstantSQLService, ConstantPostgreSQLService>();
    }
}