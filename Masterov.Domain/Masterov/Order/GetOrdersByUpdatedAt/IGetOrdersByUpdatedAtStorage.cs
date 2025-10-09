using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetOrdersByUpdatedAt;

public interface IGetOrdersByUpdatedAtStorage
{
    Task<IEnumerable<OrderDomain>?> GetOrdersByUpdatedAt(DateTime? updatedAt, CancellationToken cancellationToken);
}