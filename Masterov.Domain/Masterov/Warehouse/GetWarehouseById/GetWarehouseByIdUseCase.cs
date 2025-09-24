using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Warehouse.GetWarehouseById.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Warehouse.GetWarehouseById;

public class GetWarehouseByIdUseCase(IValidator<GetWarehouseByIdQuery> validator, IGetWarehouseByIdStorage storage) : IGetWarehouseByIdUseCase
{
    public async Task<WarehouseDomain?> Execute(GetWarehouseByIdQuery warehouseByIdQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(warehouseByIdQuery, cancellationToken);
        var warehouseExists = await storage.GetWarehouseById(warehouseByIdQuery.WarehouseId, cancellationToken);
        
        if (warehouseExists is null)
            throw new NotFoundByIdException(warehouseByIdQuery.WarehouseId, "Склад");
        
        return warehouseExists;
    }
}