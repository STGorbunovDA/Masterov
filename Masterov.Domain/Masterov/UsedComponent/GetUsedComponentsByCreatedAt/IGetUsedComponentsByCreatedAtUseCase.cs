using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByCreatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByCreatedAt;

public interface IGetUsedComponentsByCreatedAtUseCase
{
    Task<IEnumerable<UsedComponentDomain>?> Execute(GetUsedComponentsByCreatedAtQuery componentsByCreatedAtQuery, CancellationToken cancellationToken);
}