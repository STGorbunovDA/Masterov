using Masterov.Domain.Masterov.ComponentType.GetComponentTypesByCreatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.GetComponentTypesByCreatedAt;

public interface IGetComponentTypesByCreatedAtUseCase
{
    Task<IEnumerable<ComponentTypeDomain>?> Execute(GetComponentTypesByCreatedAtQuery componentTypesByCreatedAtQuery, CancellationToken cancellationToken);
}