using System.Net.Http.Headers;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace Masterov.Web.Services;

public class HttpInterceptorService(NavigationManager nav, ILocalStorageService localStorage) : DelegatingHandler
{
    private readonly NavigationManager _nav = nav;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await localStorage.GetItemAsync<string>("authToken");
        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}