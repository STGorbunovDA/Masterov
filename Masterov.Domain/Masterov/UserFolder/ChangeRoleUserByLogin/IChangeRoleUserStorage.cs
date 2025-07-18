using Masterov.Domain.Extension;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.ChangeRoleUserByName;

public interface IChangeRoleUserStorage
{
    Task<UserDomain> ChangeRoleUser(Guid userId, UserRole role, CancellationToken cancellationToken);
}