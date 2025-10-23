using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.UsedComponent.DeleteUsedComponent.Command;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentById;
using Masterov.Domain.Masterov.UsedComponent.ServiceUsedComponentAdditional;

namespace Masterov.Domain.Masterov.UsedComponent.DeleteUsedComponent;

public class DeleteUsedComponentUseCase(
    IValidator<DeleteUsedComponentCommand> validator,
    IDeleteUsedComponentStorage storage,
    IGetUsedComponentByIdStorage getUsedComponentByIdStorage,
    IWarehouseService warehouseService) : IDeleteUsedComponentUseCase
{
    public async Task<bool> Execute(DeleteUsedComponentCommand deleteUsedComponentCommand,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(deleteUsedComponentCommand, cancellationToken);

        var usedComponentExists =
            await getUsedComponentByIdStorage.GetUsedComponentById(deleteUsedComponentCommand.UsedComponentId,
                cancellationToken);

        if (usedComponentExists is null)
            throw new NotFoundByIdException(deleteUsedComponentCommand.UsedComponentId, "Заказчик");

        if (deleteUsedComponentCommand.DeleteWarehouse)
        {
          await warehouseService.ReturnQuantityWarehouse(usedComponentExists.Warehouse.WarehouseId,
                usedComponentExists.Quantity, cancellationToken);
        }

        return await storage.DeleteUsedComponent(deleteUsedComponentCommand.UsedComponentId, cancellationToken);
    }
}