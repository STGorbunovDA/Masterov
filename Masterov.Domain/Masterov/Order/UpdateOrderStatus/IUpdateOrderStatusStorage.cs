using Masterov.Domain.Extension;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.UpdateOrderStatus;

public interface IUpdateOrderStatusStorage
{
    Task<OrderDomain> UpdateOrderStatus(Guid orderId, OrderStatus orderStatus, CancellationToken cancellationToken);
}