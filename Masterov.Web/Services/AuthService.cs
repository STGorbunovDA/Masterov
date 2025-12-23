using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Text.Json;
using Blazored.LocalStorage;
using Masterov.Front.Models.Auth;

namespace Masterov.Web.Services;

public class AuthService
{
    private readonly HttpClient _http;
    private readonly ILocalStorageService _localStorage;

    public AuthService(HttpClient http, ILocalStorageService localStorage)
    {
        _http = http;
        _localStorage = localStorage;
    }

    public async Task<bool> Login(AuthLoginUserDto dto)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("api/auth/login", dto);
            if (!response.IsSuccessStatusCode) return false;

            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response JSON: {json}");
        
            var jsonDocument = JsonDocument.Parse(json);
            var root = jsonDocument.RootElement;
        
            var token = root
                .GetProperty("auth")
                .GetProperty("token")
                .GetString();
            
            await _localStorage.SetItemAsync("authToken", token);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Login error: {ex.Message}");
            return false;
        }
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("authToken");
        _http.DefaultRequestHeaders.Authorization = null;
    }
    
    public async Task<bool> IsAuthenticated()
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");
        return !string.IsNullOrEmpty(token);
    }
    
    public async Task<string?> GetRoleAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");
        if (string.IsNullOrEmpty(token))
            return null;

        try
        {
            // Убираем "Bearer " префикс если есть
            if (token.StartsWith("Bearer "))
                token = token.Substring(7);

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var roleClaim = jwtToken.Claims.FirstOrDefault(c =>
                c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");

            return roleClaim?.Value;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading token: {ex.Message}");
            return null;
        }
    }
    
    public async Task<bool> Register(AuthLoginUserDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/auth/register", dto);
        if (!response.IsSuccessStatusCode) return false;

        // Можно дополнительно обработать токен или другие данные, если нужно
        return true;
    }
}