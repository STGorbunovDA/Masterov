using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Warehouse.GetSuppliesByWarehouseId.Query;
using Masterov.Domain.Masterov.Warehouse.GetWarehouseById;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Warehouse.GetSuppliesByWarehouseId;

public class GetSuppliesByWarehouseIdUseCase(IValidator<GetSuppliesByWarehouseIdQuery> validator, IGetSuppliesByWarehouseIdStorage storage, IGetWarehouseByIdStorage getWarehouseByIdStorage) : IGetSuppliesByWarehouseIdUseCase
{
    public async Task<IEnumerable<SupplyDomain>?> Execute(GetSuppliesByWarehouseIdQuery suppliesByWarehouseIdQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(suppliesByWarehouseIdQuery, cancellationToken);
        
        var warehouseExists = await getWarehouseByIdStorage.GetWarehouseById(suppliesByWarehouseIdQuery.WarehouseId, cancellationToken);
        
        if (warehouseExists is null)
            throw new NotFoundByIdException(suppliesByWarehouseIdQuery.WarehouseId, "Склад");
        
        return await storage.GetSuppliesByWarehouseId(suppliesByWarehouseIdQuery.WarehouseId, cancellationToken);
    }
}