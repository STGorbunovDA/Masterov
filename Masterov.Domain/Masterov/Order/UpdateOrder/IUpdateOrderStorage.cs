using Masterov.Domain.Extension;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.UpdateOrder;

public interface IUpdateOrderStorage
{
    Task<OrderDomain> UpdateOrder(
        Guid orderId,
        DateTime? createdAt,
        DateTime? completedAt,
        OrderStatus status,
        string? description,
        Guid customerId,
        Guid finishedProductId,
        CancellationToken ct);
}