using Masterov.Domain.Masterov.UserFolder.GetUsersByRole.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.GetUsersByRole;

public interface IGetUsersByRoleUseCase
{
    Task<IEnumerable<UserDomain>?> Execute(GetUsersByRoleQuery usersByRoleQuery, CancellationToken cancellationToken);
}