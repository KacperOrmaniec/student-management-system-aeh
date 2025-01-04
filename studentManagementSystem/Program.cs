using studentManagementSystem.Database;
using studentManagementSystem.Abstractions;
using studentManagementSystem.Entities;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddDatabaseService(context.Configuration);
        services.AddTransient<IStudentManager, StudentManager>();
    })
    .Build();

var databaseService = host.Services.GetRequiredService<DatabaseService>();
databaseService.TestConnection();
