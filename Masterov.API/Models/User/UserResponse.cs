using Masterov.API.Models.Customer;

namespace Masterov.API.Models.User;

public class UserResponse
{
    public Guid UserId { get; set; }
    public string Login { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? AccountLoginDate { get; set; }
    public DateTime? UpdatedAt  { get; set; }
    public CustomerResponse? Customer { get; set; }
}