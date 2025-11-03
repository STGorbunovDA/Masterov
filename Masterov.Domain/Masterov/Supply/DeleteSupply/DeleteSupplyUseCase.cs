using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.ServiceAdditional.ServiceSupply;
using Masterov.Domain.Masterov.Supply.DeleteSupply.Command;
using Masterov.Domain.Masterov.Supply.GetSupplyById;

namespace Masterov.Domain.Masterov.Supply.DeleteSupply;

public class DeleteSupplyUseCase(IValidator<DeleteSupplyCommand> validator, 
    IDeleteSupplyStorage storage, IGetSupplyByIdStorage getSupplyByIdStorage,
    IUpdateWarehouseQuantityPriceSupply updateWarehouseQuantityPriceSupply) : IDeleteSupplyUseCase
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
        
        if(!result)
            throw new Conflict422Exception("Невозможно удалить поставку");
        
        var warehouse = await updateWarehouseQuantityPriceSupply.RemoveSupplyFromWarehouse(supplyExists.Warehouse.WarehouseId,
            supplyExists.Quantity, supplyExists.Price, cancellationToken);
        
        if (warehouse is null)
            throw new Conflict422Exception("Невозможно обновить склад после удаления поставки");
        
        return result;
    }
}