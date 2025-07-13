using Masterov.Domain.Masterov.ProductionOrder.AddProductionOrder.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.AddProductionOrder;

public interface IAddProductionOrderUseCase
{
    Task<ProductionOrderDomain> Execute(AddProductionOrderCommand addProductionOrderCommand, CancellationToken cancellationToken);
}