using COMPASS.Sdk.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPASS.Sdk.Interfaces.Plugins
{
    public interface ICodexContextMenuPlugin : IPlugin
    {
        public IEnumerable<CodexContextMenuItem> MenuItems { get; }
    }
}
