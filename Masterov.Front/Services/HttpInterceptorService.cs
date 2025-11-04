using System.Net.Http.Headers;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace Masterov.Front.Services;

public class HttpInterceptorService : DelegatingHandler
{
    private readonly NavigationManager _nav;
    private readonly ILocalStorageService _localStorage;

    public HttpInterceptorService(NavigationManager nav, ILocalStorageService localStorage)
    {
        _nav = nav;
        _localStorage = localStorage;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");
        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}