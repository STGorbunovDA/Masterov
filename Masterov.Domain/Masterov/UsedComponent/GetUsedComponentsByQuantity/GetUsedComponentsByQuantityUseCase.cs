using FluentValidation;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByQuantity.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByQuantity;

public class GetUsedComponentsByQuantityUseCase(IValidator<GetUsedComponentsByQuantityQuery> validator, IGetUsedComponentsByQuantityStorage storage) : IGetUsedComponentsByQuantityUseCase
{
    public async Task<IEnumerable<UsedComponentDomain?>> Execute(GetUsedComponentsByQuantityQuery usedComponentsByQuantityQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(usedComponentsByQuantityQuery, cancellationToken);
        var usedComponents = await storage.GetUsedComponentsByQuantity(usedComponentsByQuantityQuery.Quantity, cancellationToken);
        
        return usedComponents;
    }
}