using FluentValidation;
using Masterov.Domain.Masterov.Supply.GetSuppliesByUpdatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.GetSuppliesByUpdatedAt;

public class GetSuppliesByUpdatedAtUseCase(IValidator<GetSuppliesByUpdatedAtQuery> validator,
    IGetSuppliesByUpdatedAtStorage storage) : IGetSuppliesByUpdatedAtUseCase
{
    public async Task<IEnumerable<SupplyDomain>?> Execute(GetSuppliesByUpdatedAtQuery getSuppliesByUpdatedAtQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getSuppliesByUpdatedAtQuery, cancellationToken);
        return await storage.GetSuppliesByUpdatedAt(getSuppliesByUpdatedAtQuery.UpdatedAt, cancellationToken);
    }
}