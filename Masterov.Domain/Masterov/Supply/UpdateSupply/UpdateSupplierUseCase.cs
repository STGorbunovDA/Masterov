using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.ProductType.GetProductTypeById;
using Masterov.Domain.Masterov.Supplier.GetSupplierById;
using Masterov.Domain.Masterov.Supply.GetSupplyById;
using Masterov.Domain.Masterov.Supply.UpdateSupply.Command;
using Masterov.Domain.Masterov.Warehouse.GetWarehouseById;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.UpdateSupply;

public class UpdateSupplyUseCase(
    IValidator<UpdateSupplyCommand> validator,
    IUpdateSupplyStorage updateSupplyStorage,
    IGetSupplyByIdStorage getSupplyByIdStorage,
    IGetSupplierByIdStorage getSupplierByIdStorage,
    IGetProductTypeByIdStorage getProductTypeByIdStorage,
    IGetWarehouseByIdStorage getWarehouseByIdStorage) : IUpdateSupplyUseCase
{
    public async Task<SupplyDomain> Execute(UpdateSupplyCommand updateSupplyCommand,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(updateSupplyCommand, cancellationToken);
        
        var supplyExists = await getSupplyByIdStorage.GetSupplyById(updateSupplyCommand.SupplyId, cancellationToken);
        
        if (supplyExists is null)
            throw new NotFoundByIdException(updateSupplyCommand.SupplyId, "Поставка");
        
        var supplierExists = await getSupplierByIdStorage.GetSupplierById(updateSupplyCommand.SupplierId, cancellationToken);
        
        if (supplierExists is null)
            throw new NotFoundByIdException(updateSupplyCommand.SupplierId, "Поставщик");

        var productTypeExists = await getProductTypeByIdStorage.GetProductTypeById(updateSupplyCommand.ProductTypeId, cancellationToken);
        
        if (productTypeExists is null)
            throw new NotFoundByIdException(updateSupplyCommand.ProductTypeId, "Тип изделия");
        
        var warehouseExists = await getWarehouseByIdStorage.GetWarehouseById(updateSupplyCommand.WarehouseId, cancellationToken);
        
        if (warehouseExists is null)
            throw new NotFoundByIdException(updateSupplyCommand.WarehouseId, "Склад");

        return await updateSupplyStorage.UpdateSupply(updateSupplyCommand.SupplyId, updateSupplyCommand.SupplierId, updateSupplyCommand.ProductTypeId,
            updateSupplyCommand.WarehouseId, updateSupplyCommand.Quantity, updateSupplyCommand.PriceSupply, cancellationToken);
    }
}