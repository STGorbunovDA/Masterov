using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypeById;
using Masterov.Domain.Masterov.Warehouse.AddWarehouse.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Warehouse.AddWarehouse;

public class AddWarehouseUseCase(
    IValidator<AddWarehouseCommand> validator,
    IAddWarehouseStorage addWarehouseStorage,
    IGetComponentTypeByIdStorage getComponentTypeByIdStorage) : IAddWarehouseUseCase
{
    public async Task<WarehouseDomain?> Execute(AddWarehouseCommand addWarehouseCommand,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(addWarehouseCommand, cancellationToken);

        var componentTypeExists =
            await getComponentTypeByIdStorage.GetComponentTypeById(addWarehouseCommand.ComponentTypeId, cancellationToken);

        if (componentTypeExists is null)
            throw new NotFoundByIdException(addWarehouseCommand.ComponentTypeId, "Тип компонента");
        
        return await addWarehouseStorage.AddWarehouse(addWarehouseCommand.Name, addWarehouseCommand.ComponentTypeId, cancellationToken);
    }
}