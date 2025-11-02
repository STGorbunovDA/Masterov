using FluentValidation;
using Masterov.Domain.Masterov.Supply.GetSuppliesByCreatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.GetSuppliesByCreatedAt;

public class GetSuppliesByCreatedAtUseCase(IValidator<GetSuppliesByCreatedAtQuery> validator,
    IGetSuppliesByCreatedAtStorage storage) : IGetSuppliesByCreatedAtUseCase
{
    public async Task<IEnumerable<SupplyDomain>?> Execute(GetSuppliesByCreatedAtQuery getSuppliesByCreatedAtQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getSuppliesByCreatedAtQuery, cancellationToken);
        return await storage.GetSuppliesByCreatedAt(getSuppliesByCreatedAtQuery.CreatedAt, cancellationToken);
    }
}