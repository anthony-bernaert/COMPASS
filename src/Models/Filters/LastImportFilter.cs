using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPASS.Models.Filters
{
    internal class LastImportFilter : StartDateAddedFilter
    {
        public LastImportFilter(IEnumerable<Codex> newCodices) : base(newCodices.Min(x => x.DateAdded))
        {

        }

        public override string Content => "Last import";
    }
}
