using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace Masterov.Front.Services;

public class NavigationHelperService
{
    private readonly NavigationManager _navigation;
    private readonly ILocalStorageService _localStorage;

    public NavigationHelperService(NavigationManager navigation, ILocalStorageService localStorage)
    {
        _navigation = navigation;
        _localStorage = localStorage;
    }

    public async Task RedirectToLoginWithWarning()
    {
        await _localStorage.RemoveItemAsync("authToken");
        _navigation.NavigateTo("/login", true);
    }
}