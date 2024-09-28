using COMPASS.Sdk.Interfaces;
using COMPASS.Sdk.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace COMPASS.Sdk.Helpers
{
    public class ImportSourceItem
    {
        public string? Caption { get; set; }

        public string? Description { get; set; }

        public string? Icon { get; set; }

        public string? SourceName { get; set; }

        public Func<ICodexFactory, ImportProgressReporter, CancellationTokenSource, IAsyncEnumerable<ICodex>>? ImportAsync { get; set; }
    }
}
