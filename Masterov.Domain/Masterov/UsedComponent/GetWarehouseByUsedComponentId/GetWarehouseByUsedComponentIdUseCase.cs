using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentById;
using Masterov.Domain.Masterov.UsedComponent.GetWarehouseByUsedComponentId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.GetWarehouseByUsedComponentId;

public class GetWarehouseByUsedComponentIdUseCase(IValidator<GetWarehouseByUsedComponentIdQuery> validator, IGetWarehouseByUsedComponentIdStorage storage, IGetUsedComponentByIdStorage getUsedComponentByIdStorage) : IGetWarehouseByUsedComponentIdUseCase
{
    public async Task<WarehouseDomain?> Execute(GetWarehouseByUsedComponentIdQuery warehouseByUsedComponentIdQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(warehouseByUsedComponentIdQuery, cancellationToken);
        
        var usedComponentExists = await getUsedComponentByIdStorage.GetUsedComponentById(warehouseByUsedComponentIdQuery.UsedComponentId, cancellationToken);
        
        if (usedComponentExists is null)
            throw new NotFoundByIdException(warehouseByUsedComponentIdQuery.UsedComponentId, "Используемый компонент");
        
        return await storage.GetWarehouseByUsedComponentId(warehouseByUsedComponentIdQuery.UsedComponentId, cancellationToken);
    }
}