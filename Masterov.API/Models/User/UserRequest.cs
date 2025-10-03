using Masterov.API.Models.Customer;

namespace Masterov.API.Models.User;

public class UserRequest
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public CustomerRequest? Customer { get; set; }
}