using studentManagementSystem.Core.Abstractions;
using studentManagementSystem.Model.Abstractions;
using studentManagementSystem.Model.Entities;

namespace studentManagementSystem.Model;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IStudentManager, StudentManager>();

        return services;
    }
}