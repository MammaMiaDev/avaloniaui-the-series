using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaApplication.ViewModels;
using AvaloniaApplication.Views;
using CommunityToolkit.Extensions.DependencyInjection;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace AvaloniaApplication;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override async void OnFrameworkInitializationCompleted()
    {
        var locator = new ViewLocator();
        DataTemplates.Add(locator);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var services = new ServiceCollection();
            ConfigureViewModels(services);
            ConfigureViews(services);
            var provider = services.BuildServiceProvider();

            Ioc.Default.ConfigureServices(provider);

            var splashScreenVm = Ioc.Default.GetRequiredService<CustomSplashScreenViewModel>();
            var splashScreen = (Window)locator.Build(splashScreenVm);
            splashScreen.DataContext = splashScreenVm;
            desktop.MainWindow = splashScreen;
            splashScreen.Show();
            try {
                splashScreenVm.StartupMessage = "Searching for devices...";
                await Task.Delay(1000, splashScreenVm.CancellationToken);
                splashScreenVm.StartupMessage = "Connecting to device #1...";
                await Task.Delay(2000, splashScreenVm.CancellationToken);
                splashScreenVm.StartupMessage = "Configuring device...";
                await Task.Delay(2000, splashScreenVm.CancellationToken);
            }
            catch (TaskCanceledException) {
                splashScreen.Close();
                return;
            }

            var vm = Ioc.Default.GetService<MainWindowViewModel>();
            var view = (Window)locator.Build(vm);
            view.DataContext = vm;

            desktop.MainWindow = view;

            view.Show();
            splashScreen.Close();
        }

        base.OnFrameworkInitializationCompleted();
    }

    [Singleton(typeof(MainWindowViewModel))]
    [Transient(typeof(HomePageViewModel))]
    [Transient(typeof(ButtonPageViewModel))]
    [Transient(typeof(TextPageViewModel))]
    [Transient(typeof(ValueSelectionPageViewModel))]
    [Transient(typeof(ImagePageViewModel))]
    [Singleton(typeof(GridPageViewModel))]
    [Singleton(typeof(DragAndDropPageViewModel))]
    [Singleton(typeof(CustomSplashScreenViewModel))]
    internal static partial void ConfigureViewModels(IServiceCollection services);

    [Singleton(typeof(MainWindow))]
    [Transient(typeof(HomePageView))]
    [Transient(typeof(ButtonPageView))]
    [Transient(typeof(TextPageView))]
    [Transient(typeof(ValueSelectionPageView))]
    [Transient(typeof(ImagePageView))]
    [Singleton(typeof(GridPageView))]
    [Singleton(typeof(DragAndDropPageView))]
    [Singleton(typeof(CustomSplashScreenView))]
    internal static partial void ConfigureViews(IServiceCollection services);
}
