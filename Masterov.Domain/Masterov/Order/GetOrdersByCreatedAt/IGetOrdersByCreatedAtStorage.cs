using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetOrdersByCreatedAt;

public interface IGetOrdersByCreatedAtStorage
{
    Task<IEnumerable<OrderDomain>?> GetOrdersByCreatedAt(DateTime? createdAt, CancellationToken cancellationToken);
}