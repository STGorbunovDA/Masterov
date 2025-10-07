using Masterov.Domain.Masterov.Order.GetOrderById.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetOrderById;

public interface IGetOrderByOrdeIdUseCase
{
    Task<OrderDomain?> Execute(GetOrderByOrderIdQuery getOrderByOrderIdQuery, CancellationToken cancellationToken);
}