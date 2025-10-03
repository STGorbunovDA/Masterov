using FluentValidation;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByUpdatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByUpdatedAt;

public class GetFinishedProductsByUpdatedAtUseCase(IValidator<GetFinishedProductsByUpdatedAtQuery> validator,
    IGetFinishedProductsByUpdatedAtStorage storage) 
    : IGetFinishedProductsByUpdatedAtUseCase
{
    public async Task<IEnumerable<FinishedProductDomain>?> Execute(GetFinishedProductsByUpdatedAtQuery finishedProductsByUpdatedAtQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(finishedProductsByUpdatedAtQuery, cancellationToken);
        
        return await storage.GetFinishedProductsByUpdatedAt(finishedProductsByUpdatedAtQuery.UpdatedAt, cancellationToken);
    }
}