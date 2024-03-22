using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaMiaDev.ViewModels;

public partial class ButtonPageViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isButtonEnabled = true;
}
