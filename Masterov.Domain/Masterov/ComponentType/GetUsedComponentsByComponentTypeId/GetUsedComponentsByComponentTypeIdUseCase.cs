using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypeById;
using Masterov.Domain.Masterov.ComponentType.GetUsedComponentsByComponentTypeId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.GetUsedComponentsByComponentTypeId;

public class GetUsedComponentsByComponentTypeIdUseCase(IValidator<GetUsedComponentsByComponentTypeIdQuery> validator,
    IGetUsedComponentsByComponentTypeIdStorage storage, IGetComponentTypeByIdStorage getComponentTypeByIdStorage) 
    : IGetUsedComponentsByComponentTypeIdUseCase
{
    public async Task<IEnumerable<UsedComponentDomain>?> Execute(GetUsedComponentsByComponentTypeIdQuery byComponentTypeIdQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(byComponentTypeIdQuery, cancellationToken);

        var componentTypeExists = await getComponentTypeByIdStorage.GetComponentTypeById(byComponentTypeIdQuery.ComponentTypeId, cancellationToken);
        
        if (componentTypeExists is null)
            throw new NotFoundByIdException(byComponentTypeIdQuery.ComponentTypeId, "Тип компонента");

        return await storage.GetUsedComponentsByComponentTypeId(byComponentTypeIdQuery.ComponentTypeId, cancellationToken);
    }
}
