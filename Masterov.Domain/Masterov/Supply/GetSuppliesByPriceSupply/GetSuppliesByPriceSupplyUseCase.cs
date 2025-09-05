using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Supply.GetSuppliesByPriceSupply.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.GetSuppliesByPriceSupply;

public class GetSuppliesByPriceSupplyUseCase(IValidator<GetSuppliesByAmountPriceSupply> validator, IGetSuppliesByPriceSupplyStorage storage) : IGetSuppliesByPriceSupplyUseCase
{
    public async Task<IEnumerable<SupplyDomain?>> Execute(GetSuppliesByAmountPriceSupply getSuppliesByAmountPriceSupply, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getSuppliesByAmountPriceSupply, cancellationToken);
        var supplyExists = await storage.GetSuppliesByAmount(getSuppliesByAmountPriceSupply.PriceSupply, cancellationToken);
        
        if (supplyExists is null)
            throw new NotFoundByAmountException(getSuppliesByAmountPriceSupply.PriceSupply, "Платеж");
        
        return supplyExists;
    }
}