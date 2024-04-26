using System;
using System.Collections.Generic;
using LiveChartsCore;
using LiveChartsCore.Kernel;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using SkiaSharp;

namespace AvaloniaMiaDev.ViewModels.Charts;

public record WeatherForecast(DateOnly Date, int Temperature)
{
    public static IEnumerable<WeatherForecast> TestData() =>
    [
        new WeatherForecast(new DateOnly(2024, 4, 21),  4),
        new WeatherForecast(new DateOnly(2024, 4, 22), 18),
        new WeatherForecast(new DateOnly(2024, 4, 23), 21),
        new WeatherForecast(new DateOnly(2024, 4, 24), 28),
        new WeatherForecast(new DateOnly(2024, 4, 25), 19),
        new WeatherForecast(new DateOnly(2024, 4, 26), 32),
        new WeatherForecast(new DateOnly(2024, 4, 27), 35),
        new WeatherForecast(new DateOnly(2024, 4, 28), 20),
        new WeatherForecast(new DateOnly(2024, 4, 29), 30),
        new WeatherForecast(new DateOnly(2024, 4, 30), 16)
    ];
}

public class LineChartViewModel
{
    public LabelVisual Title { get; set; } =
        new()
        {
            Text = "My chart title",
            TextSize = 25,
            Padding = new LiveChartsCore.Drawing.Padding(15),
            Paint = new SolidColorPaint(SKColors.WhiteSmoke)
        };

    public ISeries[] Series { get; set; } =
    [
        new LineSeries<WeatherForecast>
        {
            Values = WeatherForecast.TestData(),
            Mapping = (sample, _) => new Coordinate(sample.Date.Day, sample.Temperature),
            Fill = null
        }
    ];

    public Axis[] XAxes { get; set; } = [ new Axis { MinStep = 1, } ];
}
