using Masterov.Domain.Extension;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.UpdateProductionOrderStatus;

public interface IUpdateProductionOrderStatusStorage
{
    Task<OrderDomain> UpdateProductionOrderStatus(Guid orderId, ProductionOrderStatus status, CancellationToken cancellationToken);
}