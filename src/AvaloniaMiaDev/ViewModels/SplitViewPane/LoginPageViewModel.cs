using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AvaloniaMiaDev.Messages;
using AvaloniaMiaDev.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace AvaloniaMiaDev.ViewModels.SplitViewPane;

public partial class LoginPageViewModel : ViewModelBase
{
    [ObservableProperty] private string _errorMessage = "";
    [ObservableProperty] private string _username = "";
    [ObservableProperty] private string _password = "";
    [ObservableProperty] private IReadOnlyList<DummyUser> _availableUsers = [];
    [ObservableProperty] private DummyUser? _selectedUser;

    partial void OnSelectedUserChanged(DummyUser? value)
    {
        if (value is null) return;

        Username = value.Username;
        Password = value.Password;
    }

    private readonly ILoginService _loginService;
    private readonly IMessenger _messenger;

    public LoginPageViewModel(ILoginService loginService, IMessenger messenger)
    {
        _loginService = loginService;
        _messenger = messenger;
        _ = GetUsers();
    }

    // design only
    public LoginPageViewModel() : this(new LoginService(new HttpClient { BaseAddress = new Uri("https://dummyjson.com/") }), new WeakReferenceMessenger()) { }

    [RelayCommand]
    private async Task Login()
    {
        var authResult = await _loginService.Authenticate(Username, Password);
        if (authResult is null)
        {
            ErrorMessage = "Invalid username or password";
            return;
        }
        ErrorMessage = "";
        _messenger.Send(new LoginSuccessMessage(authResult));
    }

    private async Task GetUsers()
    {
        AvailableUsers = await _loginService.Users();
    }
}
