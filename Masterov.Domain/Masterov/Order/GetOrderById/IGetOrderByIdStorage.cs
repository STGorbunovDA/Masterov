using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetOrderById;

public interface IGetOrderByIdStorage
{
    Task<OrderDomain?> GetOrderById(Guid orderId, CancellationToken cancellationToken);
}