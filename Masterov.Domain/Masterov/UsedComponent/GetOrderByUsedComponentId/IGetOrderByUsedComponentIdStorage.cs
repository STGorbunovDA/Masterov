using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.GetOrderByUsedComponentId;

public interface IGetOrderByUsedComponentIdStorage
{
    Task<OrderDomain?> GetOrderByUsedComponentId(Guid usedComponentId, CancellationToken cancellationToken);
}