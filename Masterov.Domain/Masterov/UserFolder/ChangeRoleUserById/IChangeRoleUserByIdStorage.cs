using Masterov.Domain.Extension;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.ChangeRoleUserById;

public interface IChangeRoleUserByIdStorage
{
    Task<UserDomain> ChangeRoleUserById(Guid userId, UserRole role, CancellationToken cancellationToken);
}