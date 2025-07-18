using Masterov.Domain.Masterov.UserFolder.ChangeRoleUserById.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.ChangeRoleUserById;

public interface IChangeRoleUserByIdUseCase
{
    Task<UserDomain> Execute(ChangeRoleUserByIdCommand changeRoleUserByNameCommand, CancellationToken cancellationToken);
}