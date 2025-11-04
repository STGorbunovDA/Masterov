using Masterov.Domain.Masterov.Warehouse.DeleteWarehouse;

namespace Masterov.Storage.Storages.Masterov.Warehouse;

internal class DeleteWarehouseStorage(MasterovDbContext dbContext) : IDeleteWarehouseStorage
{
    public async Task<bool> DeleteWarehouse(Guid warehouseId, CancellationToken cancellationToken)
    {
        var warehouse = await dbContext.Set<Storage.Warehouse>().FindAsync(new object[] { warehouseId }, cancellationToken);
        
        if (warehouse == null)
            return false;
        
        dbContext.Set<Storage.Warehouse>().Remove(warehouse);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}