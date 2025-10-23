using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Warehouse.GetWarehouseById;
using Masterov.Domain.Masterov.Warehouse.UpdateQuantityWarehouseById.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Warehouse.UpdateQuantityWarehouseById;

public class UpdateQuantityWarehouseByIdUseCase(
    IValidator<UpdateQuantityWarehouseByIdCommand> validator,
    IUpdateQuantityWarehouseByIdStorage storage,
    IGetWarehouseByIdStorage getWarehouseByIdStorage) : IUpdateQuantityWarehouseByIdUseCase
{
    public async Task<WarehouseDomain> Execute(UpdateQuantityWarehouseByIdCommand updateQuantityWarehouseByIdCommand,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(updateQuantityWarehouseByIdCommand, cancellationToken);
        
        var warehouseExists =
            await getWarehouseByIdStorage.GetWarehouseById(updateQuantityWarehouseByIdCommand.WarehouseId, cancellationToken);

        if (warehouseExists is null)
            throw new NotFoundByIdException(updateQuantityWarehouseByIdCommand.WarehouseId, "Склад");

        return await storage.UpdateQuantityWarehouseById(updateQuantityWarehouseByIdCommand.WarehouseId,
            updateQuantityWarehouseByIdCommand.Quantity, cancellationToken);
    }
}