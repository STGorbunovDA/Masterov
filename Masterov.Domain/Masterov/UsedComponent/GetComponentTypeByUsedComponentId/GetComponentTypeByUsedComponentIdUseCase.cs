using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.UsedComponent.GetComponentTypeByUsedComponentId.Query;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentById;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.GetComponentTypeByUsedComponentId;

public class GetComponentTypeByUsedComponentIdUseCase(IValidator<GetComponentTypeByUsedComponentIdQuery> validator, IGetComponentTypeByUsedComponentIdStorage storage, IGetUsedComponentByIdStorage getUsedComponentByIdStorage) : IGetComponentTypeByUsedComponentIdUseCase
{
    public async Task<ComponentTypeDomain?> Execute(GetComponentTypeByUsedComponentIdQuery componentTypeByUsedComponentIdQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(componentTypeByUsedComponentIdQuery, cancellationToken);
        
        var usedComponentExists = await getUsedComponentByIdStorage.GetUsedComponentById(componentTypeByUsedComponentIdQuery.UsedComponentId, cancellationToken);
        
        if (usedComponentExists is null)
            throw new NotFoundByIdException(componentTypeByUsedComponentIdQuery.UsedComponentId, "Используемый компонент");
        
        return await storage.GetComponentTypeByUsedComponentId(componentTypeByUsedComponentIdQuery.UsedComponentId, cancellationToken);
    }
}