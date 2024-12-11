using Avalonia;
using COMPASS.Common;
using System;
using Autofac;
using COMPASS.Common.DependencyInjection;
using COMPASS.Common.Interfaces;
using COMPASS.MacOS.EventHandlers;
using COMPASS.MacOS.Services;

namespace COMPASS.MacOS;

class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        // Create the Autofac container
        ConfigureContainer();
        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();

    private static void ConfigureContainer()
    {
        var builder = new ContainerBuilder();

        // Register Common Modules
        builder.RegisterModule<CommonModule>();

        //Register windows specific dependencies
        builder.RegisterType<EnvironmentVarsService>().As<IEnvironmentVarsService>().SingleInstance();
        builder.RegisterInstance<IWebDriverService>(new WebDriverService());

        //Don't build yet, App will also add some registrations
        App.ContainerBuilder = builder;
    }
}
