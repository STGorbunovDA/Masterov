using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Warehouse.GetWarehouseById;
using Masterov.Domain.Masterov.Warehouse.UpdatePriceWarehouseById.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Warehouse.UpdatePriceWarehouseById;

public class UpdatePriceWarehouseByIdUseCase(
    IValidator<UpdatePriceWarehouseByIdCommand> validator,
    IUpdatePriceWarehouseByIdStorage storage,
    IGetWarehouseByIdStorage getWarehouseByIdStorage) : IUpdatePriceWarehouseByIdUseCase
{
    public async Task<WarehouseDomain> Execute(UpdatePriceWarehouseByIdCommand updatePriceWarehouseByIdCommand,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(updatePriceWarehouseByIdCommand, cancellationToken);
        
        var warehouseExists =
            await getWarehouseByIdStorage.GetWarehouseById(updatePriceWarehouseByIdCommand.WarehouseId, cancellationToken);

        if (warehouseExists is null)
            throw new NotFoundByIdException(updatePriceWarehouseByIdCommand.WarehouseId, "Склад");

        return await storage.UpdatePriceWarehouseById(updatePriceWarehouseByIdCommand.WarehouseId,
            updatePriceWarehouseByIdCommand.Price, cancellationToken);
    }
}