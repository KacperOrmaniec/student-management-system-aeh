using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

namespace studentManagementSystem.Gui;

public partial class App(IServiceProvider serviceProvider) : Application
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow(_serviceProvider);
        }

        base.OnFrameworkInitializationCompleted();
    }
}