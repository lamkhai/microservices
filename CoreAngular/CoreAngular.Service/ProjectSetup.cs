using Microsoft.Extensions.DependencyInjection;

namespace CoreAngular.Service;

public static class ProjectSetup
{
    public static void AddCoreAngularServices(this IServiceCollection services)
    {
        Console.WriteLine("Add CoreAngular.Service services");
    }
}