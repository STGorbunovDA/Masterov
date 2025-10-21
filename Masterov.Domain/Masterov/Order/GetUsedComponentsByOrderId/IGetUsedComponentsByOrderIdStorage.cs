using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetUsedComponentsByOrderId;

public interface IGetUsedComponentsByOrderIdStorage
{
    Task<IEnumerable<UsedComponentDomain?>> GetUsedComponentsByOrderId(Guid orderId, CancellationToken cancellationToken);
}