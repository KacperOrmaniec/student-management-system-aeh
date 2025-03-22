using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using studentManagementSystem.Data.Abstractions;
using studentManagementSystem.Data.Database;
using studentManagementSystem.Data.Entities;
using studentManagementSystem.Data.Types;

namespace studentManagementSystem.Data;

public static class Extensions
{
    public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddTypes(configuration);
        services.AddEntities(configuration);

        return services;
    }
    
}