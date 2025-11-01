using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypeById;
using Masterov.Domain.Masterov.Order.GetOrderById;
using Masterov.Domain.Masterov.ServiceAdditional.ServiceUsedComponent;
using Masterov.Domain.Masterov.UsedComponent.AddUsedComponent.Command;
using Masterov.Domain.Masterov.Warehouse.GetWarehouseById;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.AddUsedComponent;

public class AddUsedComponentUseCase(
    IValidator<AddUsedComponentCommand> validator,
    IAddUsedComponentStorage usedComponentStorage,
    IGetOrderByIdStorage orderStorage,
    IGetComponentTypeByIdStorage componentTypeStorage,
    IGetWarehouseByIdStorage warehouseStorage,
    IUpdateWarehouseComponentQuantityPrice updateWarehouseComponentQuantityPrice)
    : IAddUsedComponentUseCase
{
    public async Task<UsedComponentDomain> Execute(AddUsedComponentCommand command, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(command, cancellationToken);

        var order = await orderStorage.GetOrderById(command.OrderId, cancellationToken);
        if (order is null)
            throw new NotFoundByIdException(command.OrderId, "Заказ");
        
        var componentType = await componentTypeStorage.GetComponentTypeById(command.ComponentTypeId, cancellationToken);
        if (componentType is null)
            throw new NotFoundByIdException(command.ComponentTypeId, "Тип изделия");
        
        var warehouse = await warehouseStorage.GetWarehouseById(command.WarehouseId, cancellationToken);
        if (warehouse is null)
            throw new NotFoundByIdException(command.WarehouseId, "Склад");

        await updateWarehouseComponentQuantityPrice.RemoveQuantityPriceWarehouse(command.WarehouseId, command.Quantity, cancellationToken);

        return await usedComponentStorage.AddUsedComponent(
            command.OrderId,
            command.ComponentTypeId,
            command.WarehouseId,
            command.Quantity,
            cancellationToken);
    }
}
