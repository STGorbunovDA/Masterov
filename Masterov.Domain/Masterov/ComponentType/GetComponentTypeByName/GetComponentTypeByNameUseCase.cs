using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypeByName.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.GetComponentTypeByName;

public class GetComponentTypeByNameUseCase(IValidator<GetComponentTypeByNameQuery> validator, IGetComponentTypeByNameStorage storage) : IGetComponentTypeByNameUseCase
{
    public async Task<ComponentTypeDomain?> Execute(GetComponentTypeByNameQuery getComponentTypeByNameQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getComponentTypeByNameQuery, cancellationToken);
        var componentTypeExists = await storage.GetComponentTypeByName(getComponentTypeByNameQuery.ComponentTypeName, cancellationToken);
        
        if (componentTypeExists is null)
            throw new NotFoundByNameException(getComponentTypeByNameQuery.ComponentTypeName, "Тип изделия");
        
        return componentTypeExists;
    }
}