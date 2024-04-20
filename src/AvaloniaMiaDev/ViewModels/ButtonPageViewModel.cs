using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaMiaDev.ViewModels;

public partial class ButtonPageViewModel : ViewModelBase, ISplitViewIcon
{
    public string IconName => "CursorHoverRegular";

    [ObservableProperty]
    private bool _isButtonEnabled = true;
}
