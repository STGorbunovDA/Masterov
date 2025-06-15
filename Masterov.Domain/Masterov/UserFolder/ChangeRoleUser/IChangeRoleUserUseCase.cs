using Masterov.Domain.Masterov.UserFolder.ChangeRoleUser.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.ChangeRoleUser;

public interface IChangeRoleUserUseCase
{
    Task<UserDomain> Execute(ChangeRoleUserCommand changeRoleUserCommand, CancellationToken cancellationToken);
}