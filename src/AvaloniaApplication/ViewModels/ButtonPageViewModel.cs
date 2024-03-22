using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaApplication.ViewModels;

public partial class ButtonPageViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isButtonEnabled = true;
}
