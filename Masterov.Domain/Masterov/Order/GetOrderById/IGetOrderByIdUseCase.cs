using Masterov.Domain.Masterov.Order.GetOrderById.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetOrderById;

public interface IGetOrderByIdUseCase
{
    Task<OrderDomain?> Execute(GetOrderByIdQuery getOrderByIdQuery, CancellationToken cancellationToken);
}