using FluentValidation;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypesByDescription.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.GetComponentTypesByDescription;

public class GetComponentTypesByDescriptionUseCase(IValidator<GetComponentTypesByDescriptionQuery> validator,
    IGetComponentTypesByDescriptionStorage storage) : IGetComponentTypesByDescriptionUseCase
{
    public async Task<IEnumerable<ComponentTypeDomain>?> Execute(GetComponentTypesByDescriptionQuery componentTypesByDescriptionQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(componentTypesByDescriptionQuery, cancellationToken);
        return await storage.GetComponentTypesByDescription(componentTypesByDescriptionQuery.Description, cancellationToken);
    }
}