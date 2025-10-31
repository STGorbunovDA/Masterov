using Masterov.Domain.Masterov.ComponentType.GetComponentTypesByUpdatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.GetComponentTypesByUpdatedAt;

public interface IGetComponentTypesByUpdatedAtUseCase
{
    Task<IEnumerable<ComponentTypeDomain>?> Execute(GetComponentTypesByUpdatedAtQuery getComponentTypesByUpdatedAtQuery, CancellationToken cancellationToken);
}