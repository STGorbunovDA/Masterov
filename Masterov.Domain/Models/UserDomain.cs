using Masterov.Domain.Extension;

namespace Masterov.Domain.Models;

public class UserDomain
{
    public Guid UserId { get; set; }
    public string Login { get; set; }
    public UserRole Role { get; set; }
    public string PasswordHash { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? AccountLoginDate { get; set; }
    public DateTime? UpdatedAt  { get; set; }
    
    public CustomerDomain? Customer { get; set; }
}
