using Masterov.Domain.Masterov.UserFolder.GetUsersByUpdatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.GetUsersByUpdatedAt;

public interface IGetUsersByUpdatedAtUseCase
{
    Task<IEnumerable<UserDomain>?> Execute(GetUsersByUpdatedAtQuery usersByUpdatedAtQuery, CancellationToken cancellationToken);
}