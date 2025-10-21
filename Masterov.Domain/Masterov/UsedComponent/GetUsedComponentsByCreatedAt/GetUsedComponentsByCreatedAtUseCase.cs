using FluentValidation;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByCreatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByCreatedAt;

public class GetUsedComponentsByCreatedAtUseCase(IValidator<GetUsedComponentsByCreatedAtQuery> validator,
    IGetUsedComponentsByCreatedAtStorage storage) : IGetUsedComponentsByCreatedAtUseCase
{
    public async Task<IEnumerable<UsedComponentDomain>?> Execute(GetUsedComponentsByCreatedAtQuery componentsByCreatedAtQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(componentsByCreatedAtQuery, cancellationToken);
        return await storage.GetUsedComponentsByCreatedAt(componentsByCreatedAtQuery.CreatedAt, cancellationToken);
    }
}