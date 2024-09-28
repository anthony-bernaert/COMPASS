using COMPASS.Sdk.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPASS.Sdk.Helpers
{
    public class CodexContextMenuItem
    {
        public string? Caption { get; set; }

        public string? Description { get; set; }

        public string? Icon { get; set; }

        public Action<IEnumerable<ICodex>>? Action { get; set; }
    }
}
