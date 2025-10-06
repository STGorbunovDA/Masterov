using Masterov.Domain.Extension;

namespace Masterov.Domain.Masterov.UserFolder.UpdateUser.Command;

public record UpdateUserCommand(Guid UserId, string Login, UserRole Role, string? NewPassword, string? OldPassword, DateTime? CreatedAt, Guid? CustomerId);