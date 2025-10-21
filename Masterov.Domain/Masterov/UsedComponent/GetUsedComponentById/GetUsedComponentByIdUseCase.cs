using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentById.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.GetUsedComponentById;

public class GetUsedComponentByIdUseCase(IValidator<GetUsedComponentByIdQuery> validator, IGetUsedComponentByIdStorage storage) : IGetUsedComponentByIdUseCase
{
    public async Task<UsedComponentDomain?> Execute(GetUsedComponentByIdQuery usedComponentByIdQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(usedComponentByIdQuery, cancellationToken);
        var usedComponentExists = await storage.GetUsedComponentById(usedComponentByIdQuery.UsedComponentId, cancellationToken);
        
        if (usedComponentExists is null)
            throw new NotFoundByIdException(usedComponentByIdQuery.UsedComponentId, "Используемый компонент");
        
        return usedComponentExists;
    }
}