using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using studentManagementSystem.Data.Abstractions;

namespace studentManagementSystem.Data.Entities;

public static class Extensions
{
    public static IServiceCollection AddEntities(this IServiceCollection services, IConfiguration configuration)
        =>  services.AddTransient<IStudentManager, StudentManager>();    
}
