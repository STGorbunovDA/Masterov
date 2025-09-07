using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Supply.GetSupplyById;
using Masterov.Domain.Masterov.Supply.GetWarehouseBySupplyId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.GetWarehouseBySupplyId;

public class GetWarehouseBySupplyIdUseCase(IValidator<GetWarehouseBySupplyIdQuery> validator, IGetWarehouseBySupplyIdStorage storage, IGetSupplyByIdStorage getSupplyByIdStorage) : IGetWarehouseBySupplyIdUseCase
{
    public async Task<WarehouseDomain?> Execute(GetWarehouseBySupplyIdQuery getWarehouseBySupplyIdQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getWarehouseBySupplyIdQuery, cancellationToken);
        
        var supplyExists = await getSupplyByIdStorage.GetSupplyById(getWarehouseBySupplyIdQuery.SupplyId, cancellationToken);
        
        if (supplyExists is null)
            throw new NotFoundByIdException(getWarehouseBySupplyIdQuery.SupplyId, "Поставка");
        
        return await storage.GetWarehouseBySupplyId(getWarehouseBySupplyIdQuery.SupplyId, cancellationToken);
    }
}