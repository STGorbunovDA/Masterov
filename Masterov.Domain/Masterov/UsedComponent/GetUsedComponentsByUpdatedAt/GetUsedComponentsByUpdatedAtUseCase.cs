using FluentValidation;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByUpdatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByUpdatedAt;

public class GetUsedComponentsByUpdatedAtUseCase(IValidator<GetUsedComponentsByUpdatedAtQuery> validator,
    IGetUsedComponentsByUpdatedAtStorage storage) : IGetUsedComponentsByUpdatedAtUseCase
{
    public async Task<IEnumerable<UsedComponentDomain>?> Execute(GetUsedComponentsByUpdatedAtQuery componentsByUpdatedAtQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(componentsByUpdatedAtQuery, cancellationToken);
        
        return await storage.GetUsedComponentsByUpdatedAt(componentsByUpdatedAtQuery.UpdatedAt, cancellationToken);
    }
}