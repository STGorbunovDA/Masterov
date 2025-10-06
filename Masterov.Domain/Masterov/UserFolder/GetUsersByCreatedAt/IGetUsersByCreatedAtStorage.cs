using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.GetUsersByCreatedAt;

public interface IGetUsersByCreatedAtStorage
{
    Task<IEnumerable<UserDomain>?> GetUsersByCreatedAt(DateTime? createdAt, CancellationToken cancellationToken);
}