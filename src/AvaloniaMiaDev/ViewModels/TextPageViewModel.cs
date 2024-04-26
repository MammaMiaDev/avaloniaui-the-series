using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaMiaDev.ViewModels;

public partial class TextPageViewModel : ViewModelBase, ISplitViewIcon
{
    public string IconName => "TextNumberFormatRegular";

    [ObservableProperty]
    private bool _isTextEnabled = true;
}
