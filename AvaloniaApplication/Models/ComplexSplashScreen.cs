using System.Threading;
using System.Threading.Tasks;
using Avalonia.Media;
using AvaloniaApplication.Views;
using FluentAvalonia.UI.Windowing;

namespace AvaloniaApplication.Models;

public class ComplexSplashScreen : IApplicationSplashScreen
{
    public ComplexSplashScreen()
    {
        SplashScreenContent = new FluentSplashScreenView();
    }

    public string AppName => "";

    public IImage? AppIcon => null;

    public object SplashScreenContent { get; }

    public int MinimumShowTime => 0;

    public async Task RunTasks(CancellationToken token)
    {
        await ((FluentSplashScreenView)SplashScreenContent).InitApp();
    }
}
