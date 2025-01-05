using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using studentManagementSystem.Data.Abstractions;
using studentManagementSystem.Data.Database;
using studentManagementSystem.Data.Types;

namespace studentManagementSystem.Data;

public static class Extensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
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
        
        services.AddDbContext<StudentDbContext>(options =>
        {
            var connectionString = configuration["database:connectionStrings:postgres"];
            options.UseNpgsql(connectionString);
        });

        services.AddTransient<IStudentRepository, StudentRepository>();

        return services;
    }
}