using FluentValidation;
using Masterov.Domain.Masterov.Supply.GetSuppliesByQuantity.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.GetSuppliesByQuantity;

public class GetSuppliesByQuantityUseCase(IValidator<GetSuppliesByQuantityQuery> validator, IGetSuppliesByQuantityStorage storage) : IGetSuppliesByQuantityUseCase
{
    public async Task<IEnumerable<SupplyDomain?>> Execute(GetSuppliesByQuantityQuery getSuppliesByQuantityQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getSuppliesByQuantityQuery, cancellationToken);
        var suppliesExists = await storage.GetSuppliesByQuantity(getSuppliesByQuantityQuery.Quantity, cancellationToken);
        
        return suppliesExists;
    }
}