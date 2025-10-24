using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Warehouse.GetWarehouseById;
using Masterov.Domain.Masterov.Warehouse.UpdateQuantityWarehouseById;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ServiceAdditional.ServiceUsedComponent;

public class UpdateWarehouseComponentQuantity (IUpdateQuantityWarehouseByIdStorage updateQuantityWarehouseByIdStorage, IGetWarehouseByIdStorage warehouseByIdStorage) : IUpdateWarehouseComponentQuantity
{
    public async Task<WarehouseDomain> RemoveQuantityWarehouse(Guid warehouseId, int quantityToUse, CancellationToken cancellationToken)
    {
        if (quantityToUse <= 0)
            throw new ArgumentException("Количество должно быть положительным", nameof(quantityToUse));
    
        var warehouse = await warehouseByIdStorage.GetWarehouseById(warehouseId, cancellationToken);
        if (warehouse is null)
            throw new NotFoundByIdException(warehouseId, "Склад");
    
        if (warehouse.Quantity < quantityToUse)
            throw new InsufficientQuantityException(warehouse.Quantity, quantityToUse);
    
        var newQuantity = warehouse.Quantity - quantityToUse;
        var updated = await updateQuantityWarehouseByIdStorage.UpdateQuantityWarehouseById(warehouseId, newQuantity, cancellationToken);
    
        if (updated is null)
            throw new Conflict409Exception("Не удалось обновить количество на складе");

        return updated;
    }

    public async Task<WarehouseDomain> ReturnQuantityWarehouse(Guid warehouseId, int quantityToUse, CancellationToken cancellationToken)
    {
        if (quantityToUse <= 0)
            throw new ArgumentException("Количество должно быть положительным", nameof(quantityToUse));
        
        var warehouse = await warehouseByIdStorage.GetWarehouseById(warehouseId, cancellationToken);
        if (warehouse is null)
            throw new NotFoundByIdException(warehouseId, "Склад");

        var newQuantity = warehouse.Quantity + quantityToUse;
        var updated = await updateQuantityWarehouseByIdStorage.UpdateQuantityWarehouseById(warehouseId, newQuantity, cancellationToken);
    
        if (updated is null)
            throw new Conflict409Exception("Не удалось обновить количество на складе");

        return updated;
    }
}