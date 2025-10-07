using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetOrderById;

public interface IGetOrderByOrderIdStorage
{
    Task<OrderDomain?> GetOrderByOrderId(Guid orderId, CancellationToken cancellationToken);
}