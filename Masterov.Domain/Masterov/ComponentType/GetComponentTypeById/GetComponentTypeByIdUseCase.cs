using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypeById.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.GetComponentTypeById;

public class GetComponentTypeByIdUseCase(IValidator<GetComponentTypeByIdQuery> validator, IGetComponentTypeByIdStorage storage) : IGetComponentTypeByIdUseCase
{
    public async Task<ComponentTypeDomain?> Execute(GetComponentTypeByIdQuery getComponentTypeByIdQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getComponentTypeByIdQuery, cancellationToken);
        var componentTypeExists = await storage.GetComponentTypeById(getComponentTypeByIdQuery.ComponentTypeId, cancellationToken);
        
        if (componentTypeExists is null)
            throw new NotFoundByIdException(getComponentTypeByIdQuery.ComponentTypeId, "Тип изделия");
        
        return componentTypeExists;
    }
}