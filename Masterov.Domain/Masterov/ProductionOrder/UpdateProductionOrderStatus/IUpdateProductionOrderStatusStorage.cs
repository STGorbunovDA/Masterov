using Masterov.Domain.Extension;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.UpdateProductionOrderStatus;

public interface IUpdateProductionOrderStatusStorage
{
    Task<ProductionOrderDomain> UpdateProductionOrderStatus(Guid orderId, ProductionOrderStatus paymentMethod, CancellationToken cancellationToken);
}