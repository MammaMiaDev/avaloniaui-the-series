using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaMiaDev.ViewModels.SplitViewPane;

public partial class TextPageViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isTextEnabled = true;
}
