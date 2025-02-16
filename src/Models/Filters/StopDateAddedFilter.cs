using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace COMPASS.Models.Filters
{
    internal class StopDateAddedFilter : Filter
    {
        public StopDateAddedFilter(DateTime dateTime) : base(FilterType.StopDateAdded, dateTime)
        {
        }

        public override Color BackgroundColor => Colors.DarkBlue;

        public override string Content => $"Added before {(FilterValue as DateTime?)?.ToShortDateString()} {(FilterValue as DateTime?)?.ToShortTimeString()}";

        public override bool Apply(Codex codex) => FilterValue is DateTime date && codex.DateAdded <= date;
    }
}
