using FluentValidation;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypesByUpdatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.GetComponentTypesByUpdatedAt;

public class GetComponentTypesByUpdatedAtUseCase(IValidator<GetComponentTypesByUpdatedAtQuery> validator,
    IGetComponentTypesByUpdatedAtStorage storage) : IGetComponentTypesByUpdatedAtUseCase
{
    public async Task<IEnumerable<ComponentTypeDomain>?> Execute(GetComponentTypesByUpdatedAtQuery componentTypesByUpdatedAtQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(componentTypesByUpdatedAtQuery, cancellationToken);
        
        return await storage.GetComponentTypesByUpdatedAt(componentTypesByUpdatedAtQuery.UpdatedAt, cancellationToken);
    }
}