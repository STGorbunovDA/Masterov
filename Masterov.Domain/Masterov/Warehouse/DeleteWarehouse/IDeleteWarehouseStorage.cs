namespace Masterov.Domain.Masterov.Warehouse.DeleteWarehouse;

public interface IDeleteWarehouseStorage
{
    Task<bool> DeleteWarehouse(Guid warehouseId, CancellationToken cancellationToken);
}