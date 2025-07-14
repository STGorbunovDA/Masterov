namespace Masterov.API.Models.Auth;

public class ChangeCustomerFromUserRequest
{
    public Guid UserId { get; set; }
    public Guid CustomerId { get; set; }
}