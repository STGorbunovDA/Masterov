using FluentValidation;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByCreatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByCreatedAt;

public class GetFinishedProductsByCreatedAtUseCase(IValidator<GetFinishedProductsByCreatedAtQuery> validator,
    IGetFinishedProductsByCreatedAtStorage storage) 
    : IGetFinishedProductsByCreatedAtUseCase
{
    public async Task<IEnumerable<FinishedProductDomain>?> Execute(GetFinishedProductsByCreatedAtQuery finishedProductsByCreatedAtQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(finishedProductsByCreatedAtQuery, cancellationToken);
        
        return await storage.GetFinishedProductsByCreatedAt(finishedProductsByCreatedAtQuery.CreatedAt, cancellationToken);
    }
}