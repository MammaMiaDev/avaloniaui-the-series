using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaMiaDev.Services;
using AvaloniaMiaDev.ViewModels;
using AvaloniaMiaDev.Views;
using CommunityToolkit.Extensions.DependencyInjection;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace AvaloniaMiaDev;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var locator = new ViewLocator();
        DataTemplates.Add(locator);

        var services = new ServiceCollection();
        ConfigureViewModels(services);
        ConfigureViews(services);
        services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);

        // Typed-clients
        // https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-8.0#typed-clients
        services.AddHttpClient<ILoginService, LoginService>(httpClient => httpClient.BaseAddress = new Uri("https://dummyjson.com/"));

        var provider = services.BuildServiceProvider();

        Ioc.Default.ConfigureServices(provider);

        var vm = Ioc.Default.GetRequiredService<MainViewModel>();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow(vm);
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView { DataContext = vm };
        }

        base.OnFrameworkInitializationCompleted();
    }

    [Singleton(typeof(MainViewModel))]
    [Transient(typeof(HomePageViewModel))]
    [Transient(typeof(ButtonPageViewModel))]
    [Transient(typeof(TextPageViewModel))]
    [Transient(typeof(ValueSelectionPageViewModel))]
    [Transient(typeof(ImagePageViewModel))]
    [Singleton(typeof(GridPageViewModel))]
    [Singleton(typeof(DragAndDropPageViewModel))]
    [Singleton(typeof(CustomSplashScreenViewModel))]
    [Singleton(typeof(LoginViewModel))]
    [Singleton(typeof(SecretViewModel))]
    internal static partial void ConfigureViewModels(IServiceCollection services);

    [Singleton(typeof(MainWindow))]
    [Transient(typeof(HomePageView))]
    [Transient(typeof(ButtonPageView))]
    [Transient(typeof(TextPageView))]
    [Transient(typeof(ValueSelectionPageView))]
    [Transient(typeof(ImagePageView))]
    [Transient(typeof(GridPageView))]
    [Transient(typeof(DragAndDropPageView))]
    [Transient(typeof(CustomSplashScreenView))]
    [Transient(typeof(LoginView))]
    [Transient(typeof(SecretView))]
    internal static partial void ConfigureViews(IServiceCollection services);
}
