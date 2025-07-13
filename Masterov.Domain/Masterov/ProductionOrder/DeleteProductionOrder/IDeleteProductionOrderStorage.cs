namespace Masterov.Domain.Masterov.ProductionOrder.DeleteProductionOrder;

public interface IDeleteProductionOrderStorage
{
    Task<bool> DeleteProductionOrder(Guid productionOrderId, CancellationToken cancellationToken);
}