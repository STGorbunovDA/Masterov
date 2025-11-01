using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypesByName.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.GetComponentTypesByName;

public class GetComponentTypesByNameUseCase(IValidator<GetComponentTypesByNameQuery> validator, IGetComponentTypeByNameStorage storage) : IGetComponentTypesByNameUseCase
{
    public async Task<IEnumerable<ComponentTypeDomain?>> Execute(GetComponentTypesByNameQuery getComponentTypesByNameQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getComponentTypesByNameQuery, cancellationToken);
        var componentTypesExists = await storage.GetComponentTypesByName(getComponentTypesByNameQuery.ComponentTypeName, cancellationToken);
        
        if (componentTypesExists is null)
            throw new NotFoundByNameException(getComponentTypesByNameQuery.ComponentTypeName, "Тип изделия");
        
        return componentTypesExists;
    }
}