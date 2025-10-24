using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypeById;
using Masterov.Domain.Masterov.Warehouse.GetWarehouseById;
using Masterov.Domain.Masterov.Warehouse.UpdateWarehouse.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Warehouse.UpdateWarehouse;

public class UpdateWarehouseUseCase(
    IValidator<UpdateWarehouseCommand> validator,
    IUpdateWarehouseStorage updateSupplyStorage,
    IGetComponentTypeByIdStorage getComponentTypeByIdStorage,
    IGetWarehouseByIdStorage getWarehouseByIdStorage) : IUpdateWarehouseUseCase
{
    public async Task<WarehouseDomain> Execute(UpdateWarehouseCommand updateWarehouseCommand,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(updateWarehouseCommand, cancellationToken);

        var productTypeExists =
            await getComponentTypeByIdStorage.GetComponentTypeById(updateWarehouseCommand.ProductTypeId, cancellationToken);

        if (productTypeExists is null)
            throw new NotFoundByIdException(updateWarehouseCommand.ProductTypeId, "Тип изделия");

        var warehouseExists =
            await getWarehouseByIdStorage.GetWarehouseById(updateWarehouseCommand.WarehouseId, cancellationToken);

        if (warehouseExists is null)
            throw new NotFoundByIdException(updateWarehouseCommand.WarehouseId, "Склад");

        return await updateSupplyStorage.UpdateWarehouse(updateWarehouseCommand.WarehouseId,
            updateWarehouseCommand.ProductTypeId, updateWarehouseCommand.Name, updateWarehouseCommand.Quantity,
            updateWarehouseCommand.Price, cancellationToken);
    }
}