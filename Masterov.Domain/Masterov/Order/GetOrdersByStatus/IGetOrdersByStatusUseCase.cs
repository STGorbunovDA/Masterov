using Masterov.Domain.Masterov.Order.GetOrdersByStatus.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetOrdersByStatus;

public interface IGetOrdersByStatusUseCase
{
    Task<IEnumerable<OrderDomain>?> Execute(GetOrdersByStatusQuery getOrdersByStatusQuery, CancellationToken cancellationToken);
}