using Masterov.Domain.Extension;

namespace Masterov.Domain.Masterov.UserFolder.ChangeRoleUserById.Command;

public record ChangeRoleUserByIdCommand(Guid UserId, UserRole Role);