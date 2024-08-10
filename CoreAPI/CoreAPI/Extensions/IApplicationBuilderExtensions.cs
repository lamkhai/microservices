using Microsoft.EntityFrameworkCore;

namespace CoreAPI.Extensions;

public static class IApplicationBuilderExtensions
{
    public static async Task MigrateDbContext<T>(this IApplicationBuilder app) where T : DbContext
    {
        using AsyncServiceScope scope = app.ApplicationServices.CreateAsyncScope();
        using T dbContext = scope.ServiceProvider.GetRequiredService<T>();
        await dbContext.Database.MigrateAsync();
    }
}