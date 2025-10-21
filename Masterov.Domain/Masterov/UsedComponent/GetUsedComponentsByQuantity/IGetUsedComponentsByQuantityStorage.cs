using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByQuantity;

public interface IGetUsedComponentsByQuantityStorage
{
    Task<IEnumerable<UsedComponentDomain?>> GetUsedComponentsByQuantity(int quantity, CancellationToken cancellationToken);
}