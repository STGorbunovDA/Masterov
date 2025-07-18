namespace Masterov.API.Models.User;

public class ChangeRoleUserByIdRequest
{
    public Guid UserId { get; set; }
    public string Role { get; set; }
}