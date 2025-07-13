using Masterov.Domain.Masterov.ProductionOrder.DeleteProductionOrder;

namespace Masterov.Storage.Storages.Masterov.ProductionOrder;

internal class DeleteProductionOrderStorage(MasterovDbContext dbContext) : IDeleteProductionOrderStorage
{
    public async Task<bool> DeleteProductionOrder(Guid productionOrderId, CancellationToken cancellationToken)
    {
        var productionOrder = await dbContext.Set<Storage.ProductionOrder>().FindAsync(new object[] { productionOrderId }, cancellationToken);
        
        if (productionOrder == null)
            return false;
        
        dbContext.Set<Storage.ProductionOrder>().Remove(productionOrder);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}
