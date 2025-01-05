using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ReactiveUI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using studentManagementSystem.Abstractions;
using studentManagementSystem.Database;
using studentManagementSystem.Entities;
using studentManagementSystemAvaloniaGui;

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

AppBuilder.Configure(() => new App(host.Services))
    .UsePlatformDetect()
    .UseReactiveUI()
    .StartWithClassicDesktopLifetime(args, ShutdownMode.OnMainWindowClose);