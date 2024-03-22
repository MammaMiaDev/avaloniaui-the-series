using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaMiaDev.ViewModels;

public partial class TextPageViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isTextEnabled = true;
}
