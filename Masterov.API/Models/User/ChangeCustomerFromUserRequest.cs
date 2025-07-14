namespace Masterov.API.Models.User;

public class ChangeCustomerFromUserRequest
{
    public Guid UserId { get; set; }
    public Guid CustomerId { get; set; }
}