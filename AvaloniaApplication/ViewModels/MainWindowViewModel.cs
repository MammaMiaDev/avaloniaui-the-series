using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaApplication.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _textBlockName = "Welcome to MammaMiaDev";
    
    [RelayCommand]
    private void ButtonOnClick()
    {
        TextBlockName = "CLICKED";
    }
}
