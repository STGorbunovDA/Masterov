namespace Masterov.API.Models.Auth;

public class AuthResponse
{
    public string Token { get; set; }
    public string Type { get; set; } = "Bearer";
    public DateTime Expires { get; set; }
}