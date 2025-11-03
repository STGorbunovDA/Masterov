using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Warehouse.GetWarehouseById;
using Masterov.Domain.Masterov.Warehouse.UpdatePriceWarehouseById;
using Masterov.Domain.Masterov.Warehouse.UpdateQuantityWarehouseById;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ServiceAdditional.ServiceUsedComponent;

public class UpdateWarehouseQuantityPriceUsedComponent (IUpdatePriceWarehouseByIdStorage updatePriceWarehouseByIdStorage,
    IUpdateQuantityWarehouseByIdStorage updateQuantityWarehouseByIdStorage,
    IGetWarehouseByIdStorage warehouseByIdStorage) : IUpdateWarehouseQuantityPriceUsedComponent
{
    // Метод использует подсчет средней цены исходя из кол-ва на складе
    public async Task<WarehouseDomain> RemoveQuantityPriceWarehouse(Guid warehouseId, int quantityToUse, CancellationToken cancellationToken)
    {
        if (quantityToUse <= 0)
            throw new ArgumentException("Количество должно быть положительным", nameof(quantityToUse));
    
        var warehouse = await warehouseByIdStorage.GetWarehouseById(warehouseId, cancellationToken);
        if (warehouse is null)
            throw new NotFoundByIdException(warehouseId, "Склад");
    
        if (warehouse.Quantity < quantityToUse)
            throw new InsufficientQuantityException(warehouse.Quantity, quantityToUse);
        
        var unitPrice = warehouse.Quantity > 0 ? decimal.Round(warehouse.Price / warehouse.Quantity, 2) : 0;
    
        var newQuantity = warehouse.Quantity - quantityToUse;
        
        var newPrice = decimal.Round(warehouse.Price - (unitPrice * quantityToUse), 2);
        
        if (newQuantity == 0)
            newPrice = 0;
        
        var updatedQuantity = await updateQuantityWarehouseByIdStorage.UpdateQuantityWarehouseById(warehouseId, newQuantity, cancellationToken);
    
        if (updatedQuantity is null)
            throw new Conflict409Exception("Не удалось обновить количество на складе");
        
        var updatedPrice = await updatePriceWarehouseByIdStorage.UpdatePriceWarehouseById(warehouseId, newPrice, cancellationToken);
    
        if (updatedQuantity is null)
            throw new Conflict409Exception("Не удалось обновить цену на складе");

        return updatedPrice;
    }
    
    // Метод использует подсчет средней цены исходя из кол-ва на складе
    public async Task<WarehouseDomain> ReturnQuantityPriceWarehouse(Guid warehouseId, int quantityToUse, CancellationToken cancellationToken)
    {
        if (quantityToUse <= 0)
            throw new ArgumentException("Количество должно быть положительным", nameof(quantityToUse));
        
        var warehouse = await warehouseByIdStorage.GetWarehouseById(warehouseId, cancellationToken);
        if (warehouse is null)
            throw new NotFoundByIdException(warehouseId, "Склад");

        var unitPrice = warehouse.Quantity > 0 ? decimal.Round(warehouse.Price / warehouse.Quantity, 2) : 0;
        var addedPrice = decimal.Round(unitPrice * quantityToUse, 2);

        var newQuantity = warehouse.Quantity + quantityToUse;
        var newPrice = warehouse.Price + addedPrice;

        var updatedQuantity = await updateQuantityWarehouseByIdStorage.UpdateQuantityWarehouseById(warehouseId, newQuantity, cancellationToken);
    
        if (updatedQuantity is null)
            throw new Conflict409Exception("Не удалось обновить количество на складе");
        
        var updatedPrice = await updatePriceWarehouseByIdStorage.UpdatePriceWarehouseById(warehouseId, newPrice, cancellationToken);
    
        if (updatedPrice is null)
            throw new Conflict409Exception("Не удалось обновить цену на складе");

        return updatedPrice;
    }
}