using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Supply.GetSupplyById.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.GetSupplyById;

public class GetSupplyByIdUseCase(IValidator<GetSupplyByIdQuery> validator, IGetSupplyByIdStorage storage) : IGetSupplyByIdUseCase
{
    public async Task<SupplyDomain?> Execute(GetSupplyByIdQuery getSupplyByIdQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getSupplyByIdQuery, cancellationToken);
        var supplyExists = await storage.GetSupplyById(getSupplyByIdQuery.SupplyId, cancellationToken);
        
        if (supplyExists is null)
            throw new NotFoundByIdException(getSupplyByIdQuery.SupplyId, "Поставка");
        
        return supplyExists;
    }
}