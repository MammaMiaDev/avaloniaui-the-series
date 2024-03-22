using System.Threading;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaMiaDev.ViewModels;

public partial class CustomSplashScreenViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _startupMessage = "Starting application...";

    public void Cancel()
    {
        StartupMessage = "Cancelling...";
        _cts.Cancel();
    }

    private readonly CancellationTokenSource _cts = new();

    public CancellationToken CancellationToken => _cts.Token;
}
