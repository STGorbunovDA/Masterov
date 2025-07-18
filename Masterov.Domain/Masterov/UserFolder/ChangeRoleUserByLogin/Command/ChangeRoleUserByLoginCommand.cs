using Masterov.Domain.Extension;

namespace Masterov.Domain.Masterov.UserFolder.ChangeRoleUserByLogin.Command;

public record ChangeRoleUserByLoginCommand(string Login, UserRole Role);