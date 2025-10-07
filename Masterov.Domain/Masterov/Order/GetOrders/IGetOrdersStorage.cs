using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetOrders;

public interface IGetOrdersStorage
{
    Task<IEnumerable<OrderDomain>> GetOrders(CancellationToken cancellationToken);
}