using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypeById;
using Masterov.Domain.Masterov.ComponentType.GetSuppliesByComponentTypeId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.GetSuppliesByComponentTypeId;

public class GetSuppliesByComponentTypeIdUseCase(IValidator<GetSuppliesByComponentTypeIdQuery> validator,
    IGetSuppliesByComponentTypeIdStorage storage, IGetComponentTypeByIdStorage getComponentTypeByIdStorage) 
    : IGetSuppliesByComponentTypeIdUseCase
{
    public async Task<IEnumerable<SupplyDomain>?> Execute(GetSuppliesByComponentTypeIdQuery suppliesByComponentTypeIdQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(suppliesByComponentTypeIdQuery, cancellationToken);

        var componentTypeExists = await getComponentTypeByIdStorage.GetComponentTypeById(suppliesByComponentTypeIdQuery.ComponentTypeId, cancellationToken);
        
        if (componentTypeExists is null)
            throw new NotFoundByIdException(suppliesByComponentTypeIdQuery.ComponentTypeId, "Тип компонента");

        return await storage.GetSuppliesByComponentTypeId(suppliesByComponentTypeIdQuery.ComponentTypeId, cancellationToken);
    }
}
