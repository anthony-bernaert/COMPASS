using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPASS.Sdk.Helpers
{
    public class PluginDescriptor
    {
        public Version? MinimumHostVersion { get; set; }

        public Version? Version { get; set; }

        public string? Author { get; set; }

        public string? FriendlyName { get; set; }

        public string? Description { get; set; }
    }
}
