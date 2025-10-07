using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetProductComponentByOrderId;

public interface IGetProductComponentByOrderIdStorage
{
    Task<IEnumerable<ProductComponentDomain?>> GetProductComponentByOrderId(Guid orderId, CancellationToken cancellationToken);
}