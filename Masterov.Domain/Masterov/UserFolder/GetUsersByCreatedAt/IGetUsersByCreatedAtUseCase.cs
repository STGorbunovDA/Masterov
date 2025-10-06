using Masterov.Domain.Masterov.UserFolder.GetUsersByCreatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.GetUsersByCreatedAt;

public interface IGetUsersByCreatedAtUseCase
{
    Task<IEnumerable<UserDomain>?> Execute(GetUsersByCreatedAtQuery usersByCreatedAtQuery, CancellationToken cancellationToken);
}