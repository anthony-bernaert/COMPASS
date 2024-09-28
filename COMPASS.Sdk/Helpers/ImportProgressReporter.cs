using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPASS.Sdk.Helpers
{
    public class ImportProgressReporter
    {
        public ImportProgressReporter(Action<int, int, string?> action)
        {
            _action = action; 
        }

        public void Report(int itemsDone, int totalItems, string? message = null)
        {
            _action(itemsDone, totalItems, message);
        }

        private readonly Action<int, int, string?> _action;
    }
}
