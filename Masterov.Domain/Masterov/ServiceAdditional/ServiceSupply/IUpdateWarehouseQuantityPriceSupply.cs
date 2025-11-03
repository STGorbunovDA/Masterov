using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ServiceAdditional.ServiceSupply;

public interface IUpdateWarehouseQuantityPriceSupply
{
    Task<WarehouseDomain> RemoveSupplyFromWarehouse(Guid warehouseId, int quantity, decimal price, CancellationToken cancellationToken);
    Task<WarehouseDomain> UpdateSupplyFromWarehouse(Guid warehouseId, int oldQuantity, decimal oldPrice, int newQuantity, decimal newPrice, CancellationToken cancellationToken);
    Task<WarehouseDomain> AddSupplyFromWarehouse(Guid warehouseId, int quantity, decimal price, CancellationToken cancellationToken);
}