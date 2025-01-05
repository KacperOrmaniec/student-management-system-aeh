using studentManagementSystem.Core.Abstractions;
using studentManagementSystem.Core.Entities;

namespace studentManagementSystem.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IStudentManager, StudentManager>();

        return services;
    }
}