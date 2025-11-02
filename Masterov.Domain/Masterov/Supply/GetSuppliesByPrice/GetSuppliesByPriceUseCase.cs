using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Supply.GetSuppliesByPrice.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.GetSuppliesByPrice;

public class GetSuppliesByPriceUseCase(IValidator<GetSuppliesByPriceQuery> validator, IGetSuppliesByPriceStorage storage) : IGetSuppliesByPriceUseCase
{
    public async Task<IEnumerable<SupplyDomain?>> Execute(GetSuppliesByPriceQuery getSuppliesByPriceQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getSuppliesByPriceQuery, cancellationToken);
        var supplyExists = await storage.GetSuppliesByPrice(getSuppliesByPriceQuery.Price, cancellationToken);
        
        if (supplyExists is null)
            throw new NotFoundByPriceException(getSuppliesByPriceQuery.Price, "Платеж");
        
        return supplyExists;
    }
}