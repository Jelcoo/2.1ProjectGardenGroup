using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;
using LiveChartsCore.SkiaSharpView.Extensions;

namespace Logic
{
    internal class DashboardLogic : ObservableObject
    {

        public IEnumerable<ISeries> Series { get; set; } =
        new[] { 2, 4, 1, 4, 3 }.AsPieSeries((value, series) =>
        {
            series.MaxRadialColumnWidth = 60;
        });
    }
}
