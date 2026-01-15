using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Masterov.Web;
using Masterov.Web.Services;
using Masterov.Web.Services.FinishedProduct;
using Microsoft.AspNetCore.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IFinishedProductService, FinishedProductService>();
// builder.Services.AddScoped<AuthService>();
// builder.Services.AddScoped<TokenService>();
// builder.Services.AddScoped<NavigationHelperService>();

builder.Services.AddScoped(sp =>
{
    var nav = sp.GetRequiredService<NavigationManager>();
    var localStorage = sp.GetRequiredService<ILocalStorageService>();
    var interceptor = new HttpInterceptorService(nav, localStorage)
    {
        InnerHandler = new HttpClientHandler()
    };

    var client = new HttpClient(interceptor)
    {
        //BaseAddress = new Uri("https://ort-pr.ru"),
        BaseAddress = new Uri("https://localhost:7214"),
        MaxResponseContentBufferSize = 1024 * 1024 * 100 // 100 MB
    };

    return client;
});

await builder.Build().RunAsync();