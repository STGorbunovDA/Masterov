namespace Masterov.Domain.Models;

public class UserDomain
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string PasswordHash { get; set; }
    
    public CustomerDomain? Customer { get; set; }
}
