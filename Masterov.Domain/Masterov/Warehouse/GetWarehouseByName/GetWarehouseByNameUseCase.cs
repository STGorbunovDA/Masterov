using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Warehouse.GetWarehouseByName.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Warehouse.GetWarehouseByName;

public class GetWarehouseByNameUseCase(IValidator<GetWarehouseByNameQuery> validator, IGetWarehouseByNameStorage storage) : IGetWarehouseByNameUseCase
{
    public async Task<IEnumerable<WarehouseDomain?>> Execute(GetWarehouseByNameQuery warehouseByNameQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(warehouseByNameQuery, cancellationToken);
        var warehouseExists = await storage.GetWarehouseByName(warehouseByNameQuery.Name, cancellationToken);
        
        if (warehouseExists is null)
            throw new NotFoundByNameException(warehouseByNameQuery.Name, "Склад");
        
        return warehouseExists;
    }
}