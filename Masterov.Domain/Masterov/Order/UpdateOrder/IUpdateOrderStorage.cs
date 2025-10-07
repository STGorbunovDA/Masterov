using Masterov.Domain.Extension;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.UpdateOrder;

public interface IUpdateOrderStorage
{
    Task<OrderDomain> UpdateOrder(
        Guid orderId, 
        DateTime createdAt, 
        OrderStatus status,
        string? description, 
        Guid customerId,
        CancellationToken ct);
}