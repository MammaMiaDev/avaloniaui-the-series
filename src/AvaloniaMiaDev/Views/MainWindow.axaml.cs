using AvaloniaMiaDev.Models;
using AvaloniaMiaDev.ViewModels;
using FluentAvalonia.UI.Windowing;

namespace AvaloniaMiaDev.Views;

public partial class MainWindow : AppWindow
{
    // constructor with 1 parameter is needed to stop the DI to instantly create the window (when declared as singleton)
    // during the startup phase and crashing the whole android app
    // with "Specified method is not supported window" error
    public MainWindow(MainViewModel vm)
    {
        DataContext = vm;
        InitializeComponent();

        TitleBar.ExtendsContentIntoTitleBar = true;
        TitleBar.TitleBarHitTestType = TitleBarHitTestType.Complex;
        
        SplashScreen = new ComplexSplashScreen();
    }
}
