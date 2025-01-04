using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using studentManagementSystem.Database;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddDatabaseService(context.Configuration);
    })
    .Build();

var databaseService = host.Services.GetRequiredService<DatabaseService>();
databaseService.TestConnection();