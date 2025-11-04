using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypeByWarehouseId.Query;
using Masterov.Domain.Masterov.Warehouse.GetWarehouseById;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.GetComponentTypeByWarehouseId;

public class GetComponentTypeByWarehouseIdUseCase(
    IValidator<GetComponentTypeByWarehouseIdQuery> validator,
    IGetComponentTypeByWarehouseIdStorage storage,
    IGetWarehouseByIdStorage getWarehouseByIdStorage) : IGetComponentTypeByWarehouseIdUseCase
{
    public async Task<ComponentTypeDomain?> Execute(
        GetComponentTypeByWarehouseIdQuery getComponentTypeByWarehouseIdIdQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getComponentTypeByWarehouseIdIdQuery, cancellationToken);
        var warehouseExists =
            await getWarehouseByIdStorage.GetWarehouseById(getComponentTypeByWarehouseIdIdQuery.WarehouseId,
                cancellationToken);

        if (warehouseExists is null)
            throw new NotFoundByIdException(getComponentTypeByWarehouseIdIdQuery.WarehouseId, "Склад");

        return await storage.GetComponentTypeByWarehouseId(getComponentTypeByWarehouseIdIdQuery.WarehouseId, cancellationToken);
    }
}