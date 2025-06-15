namespace Masterov.Domain.Models;

public class UserDomain
{
    public Guid UserId { get; set; }
    public string Login { get; set; }
    public string Role { get; set; }
    public string PasswordHash { get; set; }
}
