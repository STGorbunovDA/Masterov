using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypeById;
using Masterov.Domain.Masterov.ServiceAdditional.ServiceSupply;
using Masterov.Domain.Masterov.Supplier.GetSupplierById;
using Masterov.Domain.Masterov.Supply.AddSupply.Command;
using Masterov.Domain.Masterov.Supply.GetSupplyById;
using Masterov.Domain.Masterov.Warehouse.GetWarehouseById;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.AddSupply;

public class AddSupplyUseCase(
    IValidator<AddSupplyCommand> validator,
    IAddSupplyStorage addSupplyStorage,
    IGetSupplyByIdStorage getSupplyByIdStorage,
    IGetSupplierByIdStorage getSupplierByIdStorage,
    IGetComponentTypeByIdStorage getComponentTypeByIdStorage,
    IGetWarehouseByIdStorage getWarehouseByIdStorage,
    IUpdateWarehouseQuantityPriceSupply updateWarehouseQuantityPriceSupply) : IAddSupplyUseCase
{
    public async Task<SupplyDomain?> Execute(AddSupplyCommand addSupplyCommand,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(addSupplyCommand, cancellationToken);

        var supplierExists =
            await getSupplierByIdStorage.GetSupplierById(addSupplyCommand.SupplierId, cancellationToken);

        if (supplierExists is null)
            throw new NotFoundByIdException(addSupplyCommand.SupplierId, "Поставщик");

        var componentTypeExists =
            await getComponentTypeByIdStorage.GetComponentTypeById(addSupplyCommand.ComponentTypeId, cancellationToken);

        if (componentTypeExists is null)
            throw new NotFoundByIdException(addSupplyCommand.ComponentTypeId, "Тип компонента");

        var warehouseExists =
            await getWarehouseByIdStorage.GetWarehouseById(addSupplyCommand.WarehouseId, cancellationToken);

        if (warehouseExists is null)
            throw new NotFoundByIdException(addSupplyCommand.WarehouseId, "Склад");

        var supplyDomain = await addSupplyStorage.AddSupply(addSupplyCommand.SupplierId,
            addSupplyCommand.ComponentTypeId, addSupplyCommand.WarehouseId, addSupplyCommand.Quantity,
            addSupplyCommand.Price, cancellationToken);

        if (supplyDomain is null)
            throw new Conflict422Exception("Невозможно обработать запрос: поставка не добавлена");

        var warehouse = await updateWarehouseQuantityPriceSupply.AddSupplyFromWarehouse(addSupplyCommand.WarehouseId,
            addSupplyCommand.Quantity, addSupplyCommand.Price, cancellationToken);
        
        if (warehouse is null)
            throw new Conflict422Exception("Невозможно добавить на склад поставку");
        
        return await getSupplyByIdStorage.GetSupplyById(supplyDomain.SupplyId,cancellationToken);
    }
}