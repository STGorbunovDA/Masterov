namespace Masterov.API.Models.User;

public class ChangePasswordFromUserRequest
{
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}