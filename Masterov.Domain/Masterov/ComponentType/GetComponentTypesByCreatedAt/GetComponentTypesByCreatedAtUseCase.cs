using FluentValidation;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypesByCreatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.GetComponentTypesByCreatedAt;

public class GetComponentTypesByCreatedAtUseCase(IValidator<GetComponentTypesByCreatedAtQuery> validator,
    IGetComponentTypesByCreatedAtStorage storage) 
    : IGetComponentTypesByCreatedAtUseCase
{
    public async Task<IEnumerable<ComponentTypeDomain>?> Execute(GetComponentTypesByCreatedAtQuery componentTypesByCreatedAtQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(componentTypesByCreatedAtQuery, cancellationToken);
        return await storage.GetComponentTypesByCreatedAt(componentTypesByCreatedAtQuery.CreatedAt, cancellationToken);
    }
}