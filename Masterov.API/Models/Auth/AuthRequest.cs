namespace Masterov.API.Models.Auth;


public class AuthRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }
}