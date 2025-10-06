using Masterov.Domain.Extension;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.GetUsersByRole;

public interface IGetUsersByRoleStorage
{
    Task<IEnumerable<UserDomain>?> GetUsersByRole(UserRole role, CancellationToken cancellationToken);
}