using FluentValidation;
using Masterov.Domain.Masterov.Warehouse.GetWarehousesByCreatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Warehouse.GetWarehousesByCreatedAt;

public class GetWarehousesByCreatedAtUseCase(IValidator<GetWarehousesByCreatedAtQuery> validator,
    IGetWarehousesByCreatedAtStorage storage) : IGetWarehousesByCreatedAtUseCase
{
    public async Task<IEnumerable<WarehouseDomain>?> Execute(GetWarehousesByCreatedAtQuery warehousesByCreatedAtQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(warehousesByCreatedAtQuery, cancellationToken);
        return await storage.GetWarehousesByCreatedAt(warehousesByCreatedAtQuery.CreatedAt, cancellationToken);
    }
}