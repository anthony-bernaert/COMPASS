using COMPASS.Sdk.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COMPASS.Sdk.Interfaces.Plugins
{
    public interface IPlugin
    {
        PluginDescriptor? PluginDescriptor { get; }
    }
}