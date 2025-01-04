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

using (var scope = host.Services.CreateScope())
{
    var databaseService = host.Services.GetRequiredService<DatabaseService>();
    var dbContext = scope.ServiceProvider.GetRequiredService<StudentDbContext>();
    dbContext.Database.EnsureCreated();
}

await host.RunAsync();