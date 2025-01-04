using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace studentManagementSystem.Database;

public static class Extensions
{
    public static IServiceCollection AddDatabaseService(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddSingleton<DatabaseService>(provider =>
        {
            var connectionString = configuration["database:connectionStrings:postgres"];
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string for PostgreSQL is null or empty.");
            }
            
            return new DatabaseService(connectionString ?? throw new InvalidOperationException("Connection string not found."));
        });

        return services;
    }
}