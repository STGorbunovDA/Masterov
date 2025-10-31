using Masterov.Domain.Masterov.ComponentType.GetUsedComponentsByComponentTypeId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.GetUsedComponentsByComponentTypeId;

public interface IGetUsedComponentsByComponentTypeIdUseCase
{
    Task<IEnumerable<UsedComponentDomain>?> Execute(GetUsedComponentsByComponentTypeIdQuery usedComponentsByComponentTypeIdQuery, CancellationToken cancellationToken);
}