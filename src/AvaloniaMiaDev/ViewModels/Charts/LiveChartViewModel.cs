using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace AvaloniaMiaDev.ViewModels.Charts;

public class LiveChartViewModel
{
    private readonly Random _random = new();
    private readonly List<DateTimePoint> _values = [];
    private readonly DateTimeAxis _customAxis;

    public LiveChartViewModel()
    {
        Series = new ObservableCollection<ISeries>
        {
            new LineSeries<DateTimePoint>
            {
                Values = _values,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null
            }
        };

        _customAxis = new DateTimeAxis(TimeSpan.FromSeconds(1), Formatter)
        {
            CustomSeparators = GetSeparators(),
            AnimationsSpeed = TimeSpan.FromMilliseconds(0),
            SeparatorsPaint = new SolidColorPaint(SKColors.Black.WithAlpha(100))
        };

        XAxes = [_customAxis];

        _ = StartReadData();
        _ = DelayedStopReadData();
    }

    public ObservableCollection<ISeries> Series { get; set; }

    public Axis[] XAxes { get; set; }

    public object Sync { get; } = new();

    private bool _canRun = true;

    private async Task StartReadData()
    {
        while (_canRun)
        {
            await Task.Delay(100);

            // Because we are updating the chart from a different thread
            // we need to use a lock to access the chart data.
            // this is not necessary if your changes are made in the UI thread.
            lock (Sync)
            {
                _values.Add(new DateTimePoint(DateTime.Now, _random.Next(0, 10)));
                if (_values.Count > 50) _values.RemoveAt(0);

                // we need to update the separators every time we add a new point
                _customAxis.CustomSeparators = GetSeparators();
            }
        }
    }

    private async Task DelayedStopReadData()
    {
        await Task.Delay(6000);
        _canRun = false;
    }

    private static IEnumerable<double> GetSeparators()
    {
        var now = DateTime.Now;

        return
        [
            now.AddSeconds(-10).Ticks,
            now.AddSeconds(-5).Ticks,
            now.Ticks
        ];
    }

    private static string Formatter(DateTime date)
    {
        var secsAgo = (DateTime.Now - date).TotalSeconds;

        return secsAgo < 1
            ? "now"
            : $"{secsAgo:N0}s ago";
    }
}
