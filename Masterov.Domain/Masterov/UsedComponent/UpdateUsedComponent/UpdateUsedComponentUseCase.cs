using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypeById;
using Masterov.Domain.Masterov.Order.GetOrderById;
using Masterov.Domain.Masterov.ServiceAdditional.ServiceUsedComponent;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentById;
using Masterov.Domain.Masterov.UsedComponent.UpdateUsedComponent.Command;
using Masterov.Domain.Masterov.Warehouse.GetWarehouseById;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.UpdateUsedComponent;

public class UpdateUsedComponentUseCase(
    IValidator<UpdateUsedComponentCommand> validator,
    IUpdateUsedComponentStorage storage,
    IGetUsedComponentByIdStorage usedComponentByIdStorage,
    IGetOrderByIdStorage orderStorage,
    IGetComponentTypeByIdStorage componentTypeStorage,
    IGetWarehouseByIdStorage warehouseStorage,
    IUpdateWarehouseComponentQuantity updateWarehouseComponentQuantity) : IUpdateUsedComponentUseCase
{
    public async Task<UsedComponentDomain> Execute(UpdateUsedComponentCommand updateUsedComponentCommand,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(updateUsedComponentCommand, cancellationToken);

        var usedComponentExists = await usedComponentByIdStorage.GetUsedComponentById(updateUsedComponentCommand.UsedComponentId, cancellationToken);
        if (usedComponentExists is null)
            throw new NotFoundByIdException(updateUsedComponentCommand.UsedComponentId, "Используемый компонент");

        var order = await orderStorage.GetOrderById(updateUsedComponentCommand.OrderId, cancellationToken);
        if (order is null)
            throw new NotFoundByIdException(updateUsedComponentCommand.OrderId, "Заказ");

        var productType = await componentTypeStorage.GetComponentTypeById(updateUsedComponentCommand.ProductTypeId, cancellationToken);
        if (productType is null)
            throw new NotFoundByIdException(updateUsedComponentCommand.ProductTypeId, "Тип изделия");

        var warehouse = await warehouseStorage.GetWarehouseById(updateUsedComponentCommand.WarehouseId, cancellationToken);
        if (warehouse is null)
            throw new NotFoundByIdException(updateUsedComponentCommand.WarehouseId, "Склад");

        if (usedComponentExists.Quantity > updateUsedComponentCommand.Quantity)
           await updateWarehouseComponentQuantity.ReturnQuantityWarehouse(
                updateUsedComponentCommand.WarehouseId,
                usedComponentExists.Quantity - updateUsedComponentCommand.Quantity,
                cancellationToken);
        else if(usedComponentExists.Quantity < updateUsedComponentCommand.Quantity)
            await updateWarehouseComponentQuantity.RemoveQuantityWarehouse(
                updateUsedComponentCommand.WarehouseId,
                updateUsedComponentCommand.Quantity - usedComponentExists.Quantity,
                cancellationToken);
        
        return await storage.UpdateUsedComponent(updateUsedComponentCommand.UsedComponentId,
            updateUsedComponentCommand.OrderId, updateUsedComponentCommand.ProductTypeId,
            updateUsedComponentCommand.WarehouseId, updateUsedComponentCommand.Quantity,
            updateUsedComponentCommand.CreatedAt, cancellationToken);
    }
}