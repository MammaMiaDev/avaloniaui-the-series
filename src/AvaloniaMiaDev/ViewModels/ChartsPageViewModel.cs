using AvaloniaMiaDev.ViewModels.Charts;

namespace AvaloniaMiaDev.ViewModels;

public class ChartsPageViewModel : ViewModelBase, ISplitViewIcon
{
    public string IconName => "PollRegular";

    public LineChartViewModel LineChartViewModel { get; } = new();
    public RaceChartViewModel RaceChartViewModel { get; } = new();
    public LiveChartViewModel LiveChartViewModel { get; } = new();
}
