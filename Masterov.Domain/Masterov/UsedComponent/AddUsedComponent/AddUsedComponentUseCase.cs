using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Order.GetOrderById;
using Masterov.Domain.Masterov.ProductType.GetProductTypeById;
using Masterov.Domain.Masterov.UsedComponent.AddUsedComponent.Command;
using Masterov.Domain.Masterov.UsedComponent.ServiceUsedComponentAdditional;
using Masterov.Domain.Masterov.Warehouse.GetWarehouseById;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.AddUsedComponent;

public class AddUsedComponentUseCase(
    IValidator<AddUsedComponentCommand> validator,
    IAddUsedComponentStorage usedComponentStorage,
    IGetOrderByIdStorage orderStorage,
    IGetProductTypeByIdStorage productTypeStorage,
    IGetWarehouseByIdStorage warehouseStorage,
    IWarehouseService warehouseService)
    : IAddUsedComponentUseCase
{
    public async Task<UsedComponentDomain> Execute(AddUsedComponentCommand command, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(command, cancellationToken);

        var order = await orderStorage.GetOrderById(command.OrderId, cancellationToken);
        if (order is null)
            throw new NotFoundByIdException(command.OrderId, "Заказ");
        
        var productType = await productTypeStorage.GetProductTypeById(command.ProductTypeId, cancellationToken);
        if (productType is null)
            throw new NotFoundByIdException(command.ProductTypeId, "Тип изделия");
        
        var warehouse = await warehouseStorage.GetWarehouseById(command.WarehouseId, cancellationToken);
        if (warehouse is null)
            throw new NotFoundByIdException(command.WarehouseId, "Склад");

        await warehouseService.RemoveQuantityWarehouse(command.WarehouseId, command.Quantity, cancellationToken);

        return await usedComponentStorage.AddUsedComponent(
            command.OrderId,
            command.ProductTypeId,
            command.WarehouseId,
            command.Quantity,
            cancellationToken);
    }
}
