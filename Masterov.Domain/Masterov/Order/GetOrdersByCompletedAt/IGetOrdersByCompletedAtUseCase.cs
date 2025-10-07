using Masterov.Domain.Masterov.Order.GetOrdersByCompletedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetOrdersByCompletedAt;

public interface IGetOrdersByCompletedAtUseCase
{
    Task<IEnumerable<OrderDomain>?> Execute(GetOrdersByCompletedAtQuery getOrdersByCompletedAtQuery, CancellationToken cancellationToken);
}