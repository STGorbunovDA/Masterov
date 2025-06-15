using Masterov.Domain.Extension;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.ChangeRoleUser.Command;

public record ChangeRoleUserCommand(string Login, UserRole Role);