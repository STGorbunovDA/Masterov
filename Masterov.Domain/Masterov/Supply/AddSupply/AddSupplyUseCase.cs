using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.ProductType.GetProductTypeById;
using Masterov.Domain.Masterov.Supplier.GetSupplierById;
using Masterov.Domain.Masterov.Supply.AddSupply.Command;
using Masterov.Domain.Masterov.Warehouse.GetWarehouseById;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.AddSupply;

public class AddSupplyUseCase(
    IValidator<AddSupplyCommand> validator,
    IAddSupplyStorage addSupplyStorage,
    IGetSupplierByIdStorage getSupplierByIdStorage,
    IGetProductTypeByIdStorage getProductTypeByIdStorage,
    IGetWarehouseByIdStorage getWarehouseByIdStorage) : IAddSupplyUseCase
{
    public async Task<SupplyDomain> Execute(AddSupplyCommand addSupplyCommand,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(addSupplyCommand, cancellationToken);
        
        var supplierExists = await getSupplierByIdStorage.GetSupplierById(addSupplyCommand.SupplierId, cancellationToken);
        
        if (supplierExists is null)
            throw new NotFoundByIdException(addSupplyCommand.SupplierId, "Поставщик");

        var productTypeExists = await getProductTypeByIdStorage.GetProductTypeById(addSupplyCommand.ProductTypeId, cancellationToken);
        
        if (productTypeExists is null)
            throw new NotFoundByIdException(addSupplyCommand.ProductTypeId, "Тип изделия");
        
        var warehouseExists = await getWarehouseByIdStorage.GetWarehouseById(addSupplyCommand.WarehouseId, cancellationToken);
        
        if (warehouseExists is null)
            throw new NotFoundByIdException(addSupplyCommand.WarehouseId, "Склад");

        return await addSupplyStorage.AddSupply(addSupplyCommand.SupplierId,
            addSupplyCommand.ProductTypeId, addSupplyCommand.WarehouseId, addSupplyCommand.Quantity, addSupplyCommand.PriceSupply, cancellationToken);
    }
}