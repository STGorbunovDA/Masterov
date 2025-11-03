using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Warehouse.GetWarehouseById;
using Masterov.Domain.Masterov.Warehouse.UpdatePriceWarehouseById;
using Masterov.Domain.Masterov.Warehouse.UpdateQuantityWarehouseById;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ServiceAdditional.ServiceSupply;

public class UpdateWarehouseQuantityPriceSupply(
    IUpdatePriceWarehouseByIdStorage updatePriceWarehouseByIdStorage,
    IUpdateQuantityWarehouseByIdStorage updateQuantityWarehouseByIdStorage,
    IGetWarehouseByIdStorage warehouseByIdStorage) : IUpdateWarehouseQuantityPriceSupply
{
    public async Task<WarehouseDomain> RemoveSupplyFromWarehouse(Guid warehouseId, int quantity, decimal price,
        CancellationToken cancellationToken)
    {
        if (quantity <= 0)
            throw new ArgumentException("Количество должно быть положительным", nameof(quantity));
    
        var warehouse = await warehouseByIdStorage.GetWarehouseById(warehouseId, cancellationToken);
        if (warehouse is null)
            throw new NotFoundByIdException(warehouseId, "Склад");
    
        if (warehouse.Quantity < quantity)
            throw new InsufficientQuantityException(warehouse.Quantity, quantity);
    
        var deleteQuantity = warehouse.Quantity - quantity;

        var newPrice = warehouse.Price - price;
        
        if (deleteQuantity == 0)
            newPrice = 0;
        
        var updatedQuantity = await updateQuantityWarehouseByIdStorage.UpdateQuantityWarehouseById(warehouseId, deleteQuantity, cancellationToken);
    
        if (updatedQuantity is null)
            throw new Conflict409Exception("Не удалось удалить количество компонентов со склада");
        
        var updatedPrice = await updatePriceWarehouseByIdStorage.UpdatePriceWarehouseById(warehouseId, newPrice, cancellationToken);
    
        if (updatedQuantity is null)
            throw new Conflict409Exception("Не удалось обновить цену на складе");

        return updatedPrice;
    }

    public async Task<WarehouseDomain> UpdateSupplyFromWarehouse(Guid warehouseId, int oldQuantity, decimal oldPrice, int newQuantity, decimal newPrice, CancellationToken cancellationToken)
    {
        if (newQuantity <= 0)
            throw new ArgumentException("Новое количество должно быть положительным", nameof(newQuantity));
    
        var warehouse = await warehouseByIdStorage.GetWarehouseById(warehouseId, cancellationToken);
        if (warehouse is null)
            throw new NotFoundByIdException(warehouseId, "Склад");

        // Вычитаем старое значение
        var quantityAfterRemove = warehouse.Quantity - oldQuantity;
        var priceAfterRemove = warehouse.Price - oldPrice;

        if (quantityAfterRemove < 0)
            throw new Conflict422Exception("Количество на складе не может быть отрицательным после вычитания старой поставки.");

        // Добавляем новое значение
        var quantityAfterUpdate = quantityAfterRemove + newQuantity;
        var priceAfterUpdate = priceAfterRemove + newPrice;

        var updatedQuantity = await updateQuantityWarehouseByIdStorage.UpdateQuantityWarehouseById(warehouseId, quantityAfterUpdate, cancellationToken);
        if (updatedQuantity is null)
            throw new Conflict409Exception("Не удалось обновить количество компонентов на складе.");

        var updatedPrice = await updatePriceWarehouseByIdStorage.UpdatePriceWarehouseById(warehouseId, priceAfterUpdate, cancellationToken);
        if (updatedPrice is null)
            throw new Conflict409Exception("Не удалось обновить цену на складе.");

        return updatedPrice;
    }


    public async Task<WarehouseDomain> AddSupplyFromWarehouse(Guid warehouseId, int quantity, decimal price,
        CancellationToken cancellationToken)
    {
        if (quantity <= 0)
            throw new ArgumentException("Количество должно быть положительным", nameof(quantity));

        var warehouse = await warehouseByIdStorage.GetWarehouseById(warehouseId, cancellationToken);
        if (warehouse is null)
            throw new NotFoundByIdException(warehouseId, "Склад");

        var addQuantity =
            await updateQuantityWarehouseByIdStorage.UpdateQuantityWarehouseById(warehouseId,
                warehouse.Quantity + quantity, cancellationToken);

        if (addQuantity is null)
            throw new Conflict409Exception("Не удалось довать количество компонентов на склад");
        
        var addPrice =
            await updatePriceWarehouseByIdStorage.UpdatePriceWarehouseById(warehouseId, 
                warehouse.Price + price, cancellationToken);

        if (addPrice is null)
            throw new Conflict409Exception("Не удалось обновить цену на складе");

        return addPrice;
    }
}