using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.UsedComponent.GetProductTypeByUsedComponentId.Query;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentById;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.GetProductTypeByUsedComponentId;

public class GetProductTypeByUsedComponentIdUseCase(IValidator<GetProductTypeByUsedComponentIdQuery> validator, IGetProductTypeByUsedComponentIdStorage storage, IGetUsedComponentByIdStorage getUsedComponentByIdStorage) : IGetProductTypeByUsedComponentIdUseCase
{
    public async Task<ComponentTypeDomain?> Execute(GetProductTypeByUsedComponentIdQuery productTypeByUsedComponentIdQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(productTypeByUsedComponentIdQuery, cancellationToken);
        
        var usedComponentExists = await getUsedComponentByIdStorage.GetUsedComponentById(productTypeByUsedComponentIdQuery.UsedComponentId, cancellationToken);
        
        if (usedComponentExists is null)
            throw new NotFoundByIdException(productTypeByUsedComponentIdQuery.UsedComponentId, "Используемый компонент");
        
        return await storage.GetProductTypeByUsedComponentId(productTypeByUsedComponentIdQuery.UsedComponentId, cancellationToken);
    }
}