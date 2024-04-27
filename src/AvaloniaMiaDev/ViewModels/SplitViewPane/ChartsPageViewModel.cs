using AvaloniaMiaDev.ViewModels.Charts;

namespace AvaloniaMiaDev.ViewModels.SplitViewPane;

public class ChartsPageViewModel : ViewModelBase
{
    public LineChartViewModel LineChartViewModel { get; } = new();
    public RaceChartViewModel RaceChartViewModel { get; } = new();
    public WorldHeatMapViewModel WorldHeatMapViewModel { get; } = new();
    public LiveChartViewModel LiveChartViewModel { get; } = new();
}
