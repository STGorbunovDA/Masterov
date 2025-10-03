using Masterov.API.Models.User;

namespace Masterov.API.Models.Auth;

public class LoginResponse
{
    public AuthResponse Auth { get; set; } = new AuthResponse();
    public UserRequest User { get; set; } = new UserRequest();
}