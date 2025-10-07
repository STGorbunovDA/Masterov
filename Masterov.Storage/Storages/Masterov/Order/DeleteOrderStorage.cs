using Masterov.Domain.Masterov.Order.DeleteOrder;

namespace Masterov.Storage.Storages.Masterov.Order;

internal class DeleteOrderStorage(MasterovDbContext dbContext) : IDeleteOrderStorage
{
    public async Task<bool> DeleteOrder(Guid productionOrderId, CancellationToken cancellationToken)
    {
        var productionOrder = await dbContext.Set<Storage.Order>().FindAsync(new object[] { productionOrderId }, cancellationToken);
        
        if (productionOrder == null)
            return false;
        
        dbContext.Set<Storage.Order>().Remove(productionOrder);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}
