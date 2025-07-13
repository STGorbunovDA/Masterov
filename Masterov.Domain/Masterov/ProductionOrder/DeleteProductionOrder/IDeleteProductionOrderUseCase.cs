using Masterov.Domain.Masterov.ProductionOrder.DeleteProductionOrder.Command;

namespace Masterov.Domain.Masterov.ProductionOrder.DeleteProductionOrder;

public interface IDeleteProductionOrderUseCase
{
    Task<bool> Execute(DeleteProductionOrderCommand deleteProductionOrderCommand, CancellationToken cancellationToken);
}