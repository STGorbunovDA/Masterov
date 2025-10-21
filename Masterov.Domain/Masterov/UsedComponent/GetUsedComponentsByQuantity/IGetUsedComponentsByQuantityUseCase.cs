using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByQuantity.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByQuantity;

public interface IGetUsedComponentsByQuantityUseCase
{
    Task<IEnumerable<UsedComponentDomain?>> Execute(GetUsedComponentsByQuantityQuery usedComponentsByQuantityQuery, CancellationToken cancellationToken);
}