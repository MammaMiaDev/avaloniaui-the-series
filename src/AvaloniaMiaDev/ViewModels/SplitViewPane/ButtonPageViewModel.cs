using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaMiaDev.ViewModels.SplitViewPane;

public partial class ButtonPageViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isButtonEnabled = true;
}
