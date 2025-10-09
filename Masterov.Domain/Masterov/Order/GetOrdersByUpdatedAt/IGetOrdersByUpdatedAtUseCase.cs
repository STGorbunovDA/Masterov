using Masterov.Domain.Masterov.Order.GetOrdersByUpdatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetOrdersByUpdatedAt;

public interface IGetOrdersByUpdatedAtUseCase
{
    Task<IEnumerable<OrderDomain>?> Execute(GetOrdersByUpdatedAtQuery getOrdersByUpdatedAtQuery, CancellationToken cancellationToken);
}