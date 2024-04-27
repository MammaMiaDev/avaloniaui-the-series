using System;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.ConditionalDraw;
using LiveChartsCore.Defaults;
using LiveChartsCore.Drawing;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.Themes;
using SkiaSharp;

namespace AvaloniaMiaDev.ViewModels.Charts;

public class PilotInfo : ObservableValue
{
    public PilotInfo(string name, int value, SolidColorPaint paint)
    {
        Name = name;
        Paint = paint;
        // the ObservableValue.Value property is used by the chart
        Value = value;
    }

    public string Name { get; set; }
    public SolidColorPaint Paint { get; set; }
}

/// <summary>
/// https://livecharts.dev/docs/Avalonia/2.0.0-rc2/samples.bars.race
/// </summary>
public partial class RaceChartViewModel : ViewModelBase
{
    private readonly Random _r = new();
    private readonly PilotInfo[] _data;

    private bool _canRun = true;

    [ObservableProperty]
    private ISeries[] _series;
    [ObservableProperty]
    private Axis[] _xAxes = [new Axis { SeparatorsPaint = new SolidColorPaint(new SKColor(220, 220, 220)) }];
    [ObservableProperty]
    private Axis[] _yAxes = [new Axis { IsVisible = false }];

    public RaceChartViewModel()
    {
        // generate some paints for each pilot
        var paints = Enumerable.Range(0, 7)
            .Select(i => new SolidColorPaint(ColorPalletes.MaterialDesign500[i].AsSKColor()))
            .ToArray();

        // generate some data for each pilot
        _data =
        [
            new PilotInfo("Tsunoda",   500,  paints[0]),
            new PilotInfo("Sainz",     450,  paints[1]),
            new PilotInfo("Riccardo",  520,  paints[2]),
            new PilotInfo("Bottas",    550,  paints[3]),
            new PilotInfo("Perez",     660,  paints[4]),
            new PilotInfo("Verstapen", 920,  paints[5]),
            new PilotInfo("Hamilton",  1000, paints[6])
        ];

        // define the actual series with all the bars
        var rowSeries = new RowSeries<PilotInfo>
        {
            Values = _data.OrderBy(x => x.Value).ToArray(),
            DataLabelsPaint = new SolidColorPaint(new SKColor(245, 245, 245)),
            DataLabelsPosition = DataLabelsPosition.End,
            DataLabelsTranslate = new LvcPoint(-1, 0),
            DataLabelsFormatter = point => $"{point.Model!.Name} {point.Coordinate.PrimaryValue}",
            MaxBarWidth = 50,
            Padding = 5,
        }
        .OnPointMeasured(point =>
        {
            // assign a different color to each point
            if (point.Visual is null) return;
            point.Visual.Fill = point.Model!.Paint;
        });

        _series = [rowSeries];

        _ = StartRace();
        _ = DelayedStopRace();
    }

    private async Task StartRace()
    {
        await Task.Delay(500);

        while (_canRun)
        {
            // do a random change to the data
            foreach (var item in _data)
                item.Value += _r.Next(0, 100);

            // reset the values of the series
            Series[0].Values =
                _data.OrderBy(x => x.Value).ToArray();

            await Task.Delay(100);
        }
    }

    private async Task DelayedStopRace()
    {
        await Task.Delay(6000);
        _canRun = false;
    }
}
