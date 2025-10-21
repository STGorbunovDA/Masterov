using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByUpdatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByUpdatedAt;

public interface IGetUsedComponentsByUpdatedAtUseCase
{
    Task<IEnumerable<UsedComponentDomain>?> Execute(GetUsedComponentsByUpdatedAtQuery componentsByUpdatedAtQuery, CancellationToken cancellationToken);
}