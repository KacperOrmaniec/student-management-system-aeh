using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ReactiveUI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using studentManagementSystem.Core;
using studentManagementSystem.Data;
using studentManagementSystem.Data.Database;
using studentManagementSystem.Gui;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddDatabase(context.Configuration);
        services.AddCore(context.Configuration);
    })
    .Build();

using (var scope = host.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<StudentDbContext>();
    dbContext.Database.EnsureCreated();
}

AppBuilder.Configure(() => new App(host.Services))
    .UsePlatformDetect()
    .UseReactiveUI()
    .StartWithClassicDesktopLifetime(args, ShutdownMode.OnMainWindowClose);