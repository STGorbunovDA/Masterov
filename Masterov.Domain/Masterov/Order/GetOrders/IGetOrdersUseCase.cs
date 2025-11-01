using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetOrders;

public interface IGetOrdersUseCase
{
    Task<IEnumerable<OrderDomain?>> Execute(CancellationToken cancellationToken);
}