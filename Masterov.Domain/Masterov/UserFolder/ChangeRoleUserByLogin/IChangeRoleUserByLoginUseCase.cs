using Masterov.Domain.Masterov.UserFolder.ChangeRoleUserByLogin.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.ChangeRoleUserByLogin;

public interface IChangeRoleUserByLoginUseCase
{
    Task<UserDomain> Execute(ChangeRoleUserByLoginCommand changeRoleUserByLoginCommand, CancellationToken cancellationToken);
}