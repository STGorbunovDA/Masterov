using Masterov.Domain.Extension;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetOrdersByStatus;

public interface IGetOrdersByStatusStorage
{
    Task<IEnumerable<OrderDomain>?> GetOrdersByStatus(OrderStatus status, CancellationToken cancellationToken);
}