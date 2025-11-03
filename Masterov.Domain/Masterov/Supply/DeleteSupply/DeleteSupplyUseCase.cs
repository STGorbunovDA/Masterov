using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Supply.DeleteSupply.Command;
using Masterov.Domain.Masterov.Supply.GetSupplyById;
using Masterov.Domain.Masterov.Warehouse.UpdateWarehouse;

namespace Masterov.Domain.Masterov.Supply.DeleteSupply;

public class DeleteSupplyUseCase(IValidator<DeleteSupplyCommand> validator, 
    IDeleteSupplyStorage storage, IGetSupplyByIdStorage getSupplyByIdStorage,
    IUpdateWarehouseStorage updateWarehouseStorage) : IDeleteSupplyUseCase
{
    public async Task<bool> Execute(DeleteSupplyCommand deleteSupplyCommand, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(deleteSupplyCommand, cancellationToken);
        
        var supplyExists = await getSupplyByIdStorage.GetSupplyById(deleteSupplyCommand.SupplyId, cancellationToken);
        
        if (supplyExists is null)
            throw new NotFoundByIdException(deleteSupplyCommand.SupplyId, "Поставка");
        
        if (supplyExists.Warehouse.Quantity - supplyExists.Quantity < 0)
            throw new Conflict422Exception("Невозможно удалить поставку: количество на складе стало бы отрицательным.");

        var result = await storage.DeleteSupply(deleteSupplyCommand.SupplyId, cancellationToken);
        
        var warehouse = await updateWarehouseStorage.UpdateWarehouse(supplyExists.Warehouse.WarehouseId,
            supplyExists.Warehouse.ComponentType.ComponentTypeId, supplyExists.Warehouse.Name,
            supplyExists.Warehouse.Quantity - supplyExists.Quantity,
            supplyExists.Warehouse.Price - supplyExists.Price, cancellationToken);
        
        if (warehouse is null)
            throw new Conflict422Exception("Невозможно обработать запрос: склада не существует");
        
        return result;
    }
}