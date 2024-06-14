using CoreAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace CoreAPI.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using CoreAPIDBContext dbContext =
            scope.ServiceProvider.GetRequiredService<CoreAPIDBContext>();

        dbContext.Database.Migrate();
    }
}