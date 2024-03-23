using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace AvaloniaMiaDev.Views;

public partial class GridPageView : UserControl
{
    private readonly int _rows;
    private readonly int _cols;
    private readonly IBrush _baseColor = Brushes.LightGray;
    private bool _stop;

    public GridPageView()
    {
        InitializeComponent();

        _rows = _cols = OperatingSystem.IsAndroid() || OperatingSystem.IsIOS() ? 6 : 10;

        InitGrid();
    }

    private void InitGrid()
    {
        MainGrid.Height = 50 * _rows;
        MainGrid.Width = 50 * _cols;

        MainGrid.ShowGridLines = true;

        for (var j = 0; j < _rows; j++)
        {
            MainGrid.RowDefinitions.Add(new RowDefinition(GridLength.Star));
            MainGrid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));
            for (var i = 0; i < _cols; i++)
            {
                var child = new Rectangle
                {
                    Width = 40, Height = 40, Margin = new Thickness(1),
                    Fill = _baseColor,
                };
                MainGrid.Children.Add(child);
                Grid.SetRow(child, j);
                Grid.SetColumn(child, i);
            }
        }
    }

    private async void Start_OnClick(object? sender, RoutedEventArgs e)
    {
        _stop = false;
        Rectangle? previous = null;
        foreach (var child in MainGrid.Children)
        {
            if (_stop) break;
            if (child is not Rectangle rect) continue;
            if (previous is not null) previous.Fill = _baseColor;
            rect.Fill = Brushes.MediumSeaGreen;
            previous = rect;
            await Task.Delay(100);
        }
        if (previous is not null) previous.Fill = _baseColor;
    }

    private void Stop_OnClick(object? sender, RoutedEventArgs e)
    {
        _stop = true;
        foreach (var child in MainGrid.Children)
        {
            if (child is not Rectangle rect) continue;
            rect.Fill = _baseColor;
        }
    }
}
