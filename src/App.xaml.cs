using Autofac;
using COMPASS.Interfaces;
using COMPASS.Models.Enums;
using COMPASS.Services;
using COMPASS.Tools;
using COMPASS.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using COMPASS.Windows;
using COMPASS.Sdk.Interfaces.Plugins;
using COMPASS.Infrastructure;

namespace COMPASS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            try
            {
                if (!Directory.Exists(SettingsViewModel.CompassDataPath))
                {
                    Directory.CreateDirectory(SettingsViewModel.CompassDataPath);
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to create folder at compass data path, so data cannot be saved", ex);
                string msg = $"Failed to create a folder to store user data at {SettingsViewModel.CompassDataPath}, " +
                             $"please pick a new location to save your data. Creation failed with the following error {ex.Message}";
                IOService.AskNewCodexFilePath(msg);
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //init the container
            var builder = new ContainerBuilder();

            builder.RegisterType<ApplicationDispatcher>().As<IDispatcher>();
            builder.RegisterType<WindowedNotificationService>().Keyed<INotificationService>(NotificationDisplayType.Windowed);
            builder.RegisterType<WindowedNotificationService>().Keyed<INotificationService>(NotificationDisplayType.Toast); //use windowed for everything for now

            // Register MainView and MainViewModel
            builder.RegisterType<MainWindow>().AsSelf().SingleInstance();
            builder.RegisterType<MainViewModel>().AsSelf().InstancePerDependency();

            // Load the plugins and register them in the IoC container
            var pluginLoader = new PluginLoader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins"));
            pluginLoader.LoadAndRegisterPlugins(builder);

            _container = builder.Build();

            var mainWindow = _container.Resolve<MainWindow>();
            mainWindow.Show();
        }

        private static IContainer? _container;

        public static IContainer Container
        {
            get => _container;
            set => _container = value;
        }

        private static IDispatcher? _dispatcher;
        public static IDispatcher SafeDispatcher => _dispatcher ??= Container.Resolve<IDispatcher>();

        #region Plugin loading
        
        #endregion
    }
}
