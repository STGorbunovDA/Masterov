using Masterov.Domain.Masterov.Order.GetOrdersByCreatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetOrdersByCreatedAt;

public interface IGetOrdersByCreatedAtUseCase
{
    Task<IEnumerable<OrderDomain>?> Execute(GetOrdersByCreatedAtQuery getOrdersByCreatedAtQuery, CancellationToken cancellationToken);
}