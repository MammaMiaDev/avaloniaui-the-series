using AvaloniaApplication.Models;
using FluentAvalonia.UI.Windowing;

namespace AvaloniaApplication.Views;

public partial class MainWindow : AppWindow
{
    public MainWindow()
    {
        InitializeComponent();

        TitleBar.ExtendsContentIntoTitleBar = true;
        TitleBar.TitleBarHitTestType = TitleBarHitTestType.Complex;
        
        SplashScreen = new ComplexSplashScreen();
    }
}
