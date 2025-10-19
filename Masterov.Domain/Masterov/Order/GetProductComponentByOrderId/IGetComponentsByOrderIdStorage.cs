using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetProductComponentByOrderId;

public interface IGetComponentsByOrderIdStorage
{
    Task<IEnumerable<ComponentsDomain?>> GetComponentsByOrderId(Guid orderId, CancellationToken cancellationToken);
}