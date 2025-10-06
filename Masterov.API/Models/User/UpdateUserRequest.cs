namespace Masterov.API.Models.User;

public class UpdateUserRequest
{
    public string Login { get; set; }
    public string Role { get; set; }
    public string? NewPassword { get; set; }
    public string? OldPassword { get; set; }
    public string? CreatedAt { get; set; }
    public Guid? CustomerId { get; set; }
}