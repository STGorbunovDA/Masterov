using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Warehouse.DeleteWarehouse.Command;
using Masterov.Domain.Masterov.Warehouse.GetWarehouseById;

namespace Masterov.Domain.Masterov.Warehouse.DeleteWarehouse;

public class DeleteWarehouseUseCase(IValidator<DeleteWarehouseCommand> validator, 
    IDeleteWarehouseStorage storage, IGetWarehouseByIdStorage getWarehouseByIdStorage) : IDeleteWarehouseUseCase
{
    public async Task<bool> Execute(DeleteWarehouseCommand deleteWarehouseCommand, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(deleteWarehouseCommand, cancellationToken);
        
        var warehouseExists = await getWarehouseByIdStorage.GetWarehouseById(deleteWarehouseCommand.WarehouseId, cancellationToken);
        
        if (warehouseExists is null)
            throw new NotFoundByIdException(deleteWarehouseCommand.WarehouseId, "Склад");
        
        if (warehouseExists.Supplies.Any() || warehouseExists.Quantity > 0 || warehouseExists.Price > 0)
            throw new Conflict422Exception("Невозможно удалить склад: Склад не пуст.");
        
        return await storage.DeleteWarehouse(deleteWarehouseCommand.WarehouseId, cancellationToken);;
    }
}