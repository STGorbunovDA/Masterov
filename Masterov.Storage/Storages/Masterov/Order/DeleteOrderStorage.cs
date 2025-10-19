using Masterov.Domain.Masterov.Order.DeleteOrder;

namespace Masterov.Storage.Storages.Masterov.Order;

internal class DeleteOrderStorage(MasterovDbContext dbContext) : IDeleteOrderStorage
{
    public async Task<bool> DeleteOrder(Guid productionOrderId, CancellationToken cancellationToken)
    {
        var order = await dbContext.Set<Storage.Order>().FindAsync(new object[] { productionOrderId }, cancellationToken);
        
        if (order == null)
            return false;
        
        dbContext.Set<Storage.Order>().Remove(order);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}
