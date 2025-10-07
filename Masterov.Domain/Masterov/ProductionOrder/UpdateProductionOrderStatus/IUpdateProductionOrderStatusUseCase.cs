using Masterov.Domain.Masterov.ProductionOrder.UpdateProductionOrderStatus.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.UpdateProductionOrderStatus;

public interface IUpdateProductionOrderStatusUseCase
{
    Task<OrderDomain> Execute(UpdateProductionOrderStatusCommand updateProductionOrderStatusCommand, CancellationToken cancellationToken);
}