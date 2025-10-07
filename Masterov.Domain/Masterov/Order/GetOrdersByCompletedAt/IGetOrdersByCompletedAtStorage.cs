using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetOrdersByCompletedAt;

public interface IGetOrdersByCompletedAtStorage
{
    Task<IEnumerable<OrderDomain>?> GetOrdersByCompletedAt(DateTime completedAt, CancellationToken cancellationToken);
}