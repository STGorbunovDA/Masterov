using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.ServiceAdditional.ServiceUsedComponent;
using Masterov.Domain.Masterov.UsedComponent.DeleteUsedComponent.Command;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentById;

namespace Masterov.Domain.Masterov.UsedComponent.DeleteUsedComponent;

public class DeleteUsedComponentUseCase(
    IValidator<DeleteUsedComponentCommand> validator,
    IDeleteUsedComponentStorage storage,
    IGetUsedComponentByIdStorage getUsedComponentByIdStorage,
    IUpdateWarehouseComponentQuantityPrice updateWarehouseComponentQuantityPrice) : IDeleteUsedComponentUseCase
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
          await updateWarehouseComponentQuantityPrice.ReturnQuantityPriceWarehouse(usedComponentExists.Warehouse.WarehouseId,
                usedComponentExists.Quantity, cancellationToken);
        }

        return await storage.DeleteUsedComponent(deleteUsedComponentCommand.UsedComponentId, cancellationToken);
    }
}