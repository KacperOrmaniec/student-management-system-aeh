using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using studentManagementSystem.Data.Abstractions;

namespace studentManagementSystem.Data.Types;

public static class Extensions
{
    public static IServiceCollection AddTypes(this IServiceCollection services, IConfiguration configuration)
        => services.AddTransient<IStudentRepository, StudentRepository>();
}
