using Masterov.API.Models.Customer;

namespace Masterov.API.Models.User;

public class UserRequest
{
    public Guid UserId { get; set; }
    public string Login { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public CustomerRequest? Customer { get; set; }
}