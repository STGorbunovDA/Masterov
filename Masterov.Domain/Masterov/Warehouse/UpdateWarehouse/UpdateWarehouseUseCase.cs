using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypeById;
using Masterov.Domain.Masterov.Warehouse.GetWarehouseById;
using Masterov.Domain.Masterov.Warehouse.UpdateWarehouse.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Warehouse.UpdateWarehouse;

public class UpdateWarehouseUseCase(
    IValidator<UpdateWarehouseCommand> validator,
    IUpdateWarehouseStorage updateWarehouseStorage,
    IGetComponentTypeByIdStorage getComponentTypeByIdStorage,
    IGetWarehouseByIdStorage getWarehouseByIdStorage) : IUpdateWarehouseUseCase
{
    public async Task<WarehouseDomain> Execute(UpdateWarehouseCommand updateWarehouseCommand,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(updateWarehouseCommand, cancellationToken);

        var componentTypeExists =
            await getComponentTypeByIdStorage.GetComponentTypeById(updateWarehouseCommand.ComponentTypeId, cancellationToken);

        if (componentTypeExists is null)
            throw new NotFoundByIdException(updateWarehouseCommand.ComponentTypeId, "Тип изделия");

        var warehouseExists =
            await getWarehouseByIdStorage.GetWarehouseById(updateWarehouseCommand.WarehouseId, cancellationToken);

        if (warehouseExists is null)
            throw new NotFoundByIdException(updateWarehouseCommand.WarehouseId, "Склад");

        return await updateWarehouseStorage.UpdateWarehouse(updateWarehouseCommand.WarehouseId,
            updateWarehouseCommand.ComponentTypeId, updateWarehouseCommand.Name, updateWarehouseCommand.Quantity,
            updateWarehouseCommand.Price, updateWarehouseCommand.CreatedAt, cancellationToken);
    }
}