using FluentValidation;
using Masterov.Domain.Masterov.Warehouse.GetWarehousesByUpdatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Warehouse.GetWarehousesByUpdatedAt;

public class GetWarehousesByUpdatedAtUseCase(IValidator<GetWarehousesByUpdatedAtQuery> validator,
    IGetWarehousesByUpdatedAtStorage storage) : IGetWarehousesByUpdatedAtUseCase
{
    public async Task<IEnumerable<WarehouseDomain>?> Execute(GetWarehousesByUpdatedAtQuery warehousesByUpdatedAtQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(warehousesByUpdatedAtQuery, cancellationToken);
        return await storage.GetWarehousesByUpdatedAt(warehousesByUpdatedAtQuery.UpdatedAt, cancellationToken);
    }
}