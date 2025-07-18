using Masterov.Domain.Masterov.ProductionOrder.UpdateProductionOrder.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.UpdateProductionOrder;

public interface IUpdateProductionOrderUseCase
{
    Task<ProductionOrderDomain> Execute(UpdateProductionOrderCommand command, CancellationToken cancellationToken);
}