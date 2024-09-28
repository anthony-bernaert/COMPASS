using Autofac;
using COMPASS.Sdk.Interfaces.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace COMPASS.Infrastructure
{
    public class PluginLoader
    {
        private readonly string _pluginPath;

        public PluginLoader(string pluginPath)
        {
            _pluginPath = pluginPath;
        }

        public void LoadAndRegisterPlugins(ContainerBuilder builder)
        {
            if (!Path.Exists(_pluginPath))
                return;

            var pluginDirectories = Directory.GetDirectories(_pluginPath);
            foreach (var pluginDirectory in pluginDirectories)
            {
                var pluginFiles = Directory.GetFiles(pluginDirectory, "*.dll");

                foreach (var pluginFile in pluginFiles)
                {
                    var assembly = Assembly.LoadFrom(pluginFile);
                    var pluginTypes = assembly.GetTypes().Where(t => typeof(IPlugin).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

                    foreach (var pluginType in pluginTypes)
                    {
                        var plugin = (IPlugin)Activator.CreateInstance(pluginType)!;
                        builder.RegisterInstance(plugin).As<IPlugin>();
                    }
                }
            }
        }
    }
}
