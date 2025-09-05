using FluentValidation;
using Masterov.Domain.Masterov.Supply.GetSuppliesBySupplyDate.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.GetSuppliesBySupplyDate;

public class GetSuppliesBySupplyDateUseCase(IValidator<GetSuppliesBySupplyDateQuery> validator,
    IGetSuppliesBySupplyDateStorage storage) : IGetSuppliesBySupplyDateUseCase
{
    public async Task<IEnumerable<SupplyDomain>?> Execute(GetSuppliesBySupplyDateQuery getSuppliesBySupplyDateQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getSuppliesBySupplyDateQuery, cancellationToken);
        
        return await storage.GetSuppliesBySupplyDate(getSuppliesBySupplyDateQuery.SupplyDate, cancellationToken);
    }
}