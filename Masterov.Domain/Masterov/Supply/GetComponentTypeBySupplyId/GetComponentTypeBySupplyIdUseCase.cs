using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Supply.GetComponentTypeBySupplyId.Query;
using Masterov.Domain.Masterov.Supply.GetSupplyById;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.GetComponentTypeBySupplyId;

public class GetComponentTypeBySupplyIdUseCase(IValidator<GetComponentTypeBySupplyIdQuery> validator, IGetComponentTypeBySupplyIdStorage storage, IGetSupplyByIdStorage getSupplyByIdStorage) : IGetComponentTypeBySupplyIdUseCase
{
    public async Task<ComponentTypeDomain?> Execute(GetComponentTypeBySupplyIdQuery getComponentTypeBySupplyIdQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getComponentTypeBySupplyIdQuery, cancellationToken);
        
        var supplyExists = await getSupplyByIdStorage.GetSupplyById(getComponentTypeBySupplyIdQuery.SupplyId, cancellationToken);
        
        if (supplyExists is null)
            throw new NotFoundByIdException(getComponentTypeBySupplyIdQuery.SupplyId, "Поставка");
        
        return await storage.GetComponentTypeBySupplyId(getComponentTypeBySupplyIdQuery.SupplyId, cancellationToken);
    }
}