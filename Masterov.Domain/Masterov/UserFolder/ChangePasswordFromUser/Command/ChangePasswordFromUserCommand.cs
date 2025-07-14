namespace Masterov.Domain.Masterov.UserFolder.ChangePasswordFromUser.Command;

public record ChangePasswordFromUserCommand(Guid UserId, string OldPassword, string NewPassword);