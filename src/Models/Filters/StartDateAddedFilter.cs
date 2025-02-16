using COMPASS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace COMPASS.Models.Filters
{
    internal class StartDateAddedFilter : Filter
    {
        public StartDateAddedFilter(DateTime dateTime) : base(FilterType.StartDateAdded, dateTime)
        {
        }

        public override Color BackgroundColor => Colors.DarkOliveGreen;

        public override string Content => $"Added after {(FilterValue as DateTime?)?.ToShortDateString()} {(FilterValue as DateTime?)?.ToShortTimeString() }";

        public override bool Apply(Codex codex) => FilterValue is DateTime date && codex.DateAdded >= date;
    }
}
