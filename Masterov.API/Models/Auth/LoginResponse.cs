using Masterov.API.Models.User;

namespace Masterov.API.Models.Auth;

public class LoginResponse
{
    public AuthResponse Auth { get; set; } = new AuthResponse();
    public UserResponse User { get; set; } = new UserResponse();
}