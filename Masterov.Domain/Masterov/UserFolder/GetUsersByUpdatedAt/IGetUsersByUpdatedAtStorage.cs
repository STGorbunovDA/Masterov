using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.GetUsersByUpdatedAt;

public interface IGetUsersByUpdatedAtStorage
{
    Task<IEnumerable<UserDomain>?> GetUsersByUpdatedAt(DateTime? updatedAt, CancellationToken cancellationToken);
}