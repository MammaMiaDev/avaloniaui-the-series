using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaMiaDev.ViewModels;

public partial class ValueSelectionPageViewModel : ViewModelBase, ISplitViewIcon
{
    public string IconName => "CalendarCheckmarkRegular";

    [ObservableProperty]
    private bool _isValueSelectionEnabled = true;

    [ObservableProperty]
    private int _sliderValue;
}
