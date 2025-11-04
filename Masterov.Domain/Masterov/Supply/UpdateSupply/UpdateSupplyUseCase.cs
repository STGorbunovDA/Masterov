using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypeById;
using Masterov.Domain.Masterov.ServiceAdditional.ServiceSupply;
using Masterov.Domain.Masterov.Supplier.GetSupplierById;
using Masterov.Domain.Masterov.Supply.GetSupplyById;
using Masterov.Domain.Masterov.Supply.UpdateSupply;
using Masterov.Domain.Masterov.Supply.UpdateSupply.Command;
using Masterov.Domain.Masterov.Warehouse.GetWarehouseById;
using Masterov.Domain.Models;

public class UpdateSupplyUseCase(
    IValidator<UpdateSupplyCommand> validator,
    IUpdateSupplyStorage updateSupplyStorage,
    IGetSupplyByIdStorage getSupplyByIdStorage,
    IGetSupplierByIdStorage getSupplierByIdStorage,
    IGetComponentTypeByIdStorage getComponentTypeByIdStorage,
    IGetWarehouseByIdStorage getWarehouseByIdStorage,
    IUpdateWarehouseQuantityPriceSupply updateWarehouseQuantityPriceSupply) : IUpdateSupplyUseCase
{
    public async Task<SupplyDomain?> Execute(UpdateSupplyCommand updateSupplyCommand, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(updateSupplyCommand, cancellationToken);

        var oldSupply = await getSupplyByIdStorage.GetSupplyById(updateSupplyCommand.SupplyId, cancellationToken);
        if (oldSupply is null)
            throw new NotFoundByIdException(updateSupplyCommand.SupplyId, "Поставка");

        var supplierExists = await getSupplierByIdStorage.GetSupplierById(updateSupplyCommand.SupplierId, cancellationToken);
        if (supplierExists is null)
            throw new NotFoundByIdException(updateSupplyCommand.SupplierId, "Поставщик");

        var componentTypeExists = await getComponentTypeByIdStorage.GetComponentTypeById(updateSupplyCommand.ComponentTypeId, cancellationToken);
        if (componentTypeExists is null)
            throw new NotFoundByIdException(updateSupplyCommand.ComponentTypeId, "Тип изделия");

        var warehouseExists = await getWarehouseByIdStorage.GetWarehouseById(updateSupplyCommand.WarehouseId, cancellationToken);
        if (warehouseExists is null)
            throw new NotFoundByIdException(updateSupplyCommand.WarehouseId, "Склад");

        // Обновляем саму поставку
        var updatedSupply = await updateSupplyStorage.UpdateSupply(
            updateSupplyCommand.SupplyId,
            updateSupplyCommand.SupplierId,
            updateSupplyCommand.ComponentTypeId,
            updateSupplyCommand.WarehouseId,
            updateSupplyCommand.Quantity,
            updateSupplyCommand.Price,
            updateSupplyCommand.CreatedAt,
            cancellationToken);

        // Пересчитываем склад
        var warehouse = await updateWarehouseQuantityPriceSupply.UpdateSupplyFromWarehouse(
            oldSupply.Warehouse.WarehouseId,
            oldSupply.Quantity,
            oldSupply.Price,
            updateSupplyCommand.Quantity,
            updateSupplyCommand.Price,
            cancellationToken);

        if (warehouse is null)
            throw new Conflict422Exception("Не удалось обновить склад при изменении поставки.");

        return await getSupplyByIdStorage.GetSupplyById(updateSupplyCommand.SupplyId, cancellationToken);;
    }
}
