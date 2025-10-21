using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.UsedComponent.GetOrderByUsedComponentId.Query;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentById;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.GetOrderByUsedComponentId;

public class GetOrderByUsedComponentIdUseCase(IValidator<GetOrderByUsedComponentIdQuery> validator,
    IGetOrderByUsedComponentIdStorage storage, IGetUsedComponentByIdStorage getCustomerByIdStorage) 
    : IGetOrderByUsedComponentIdUseCase
{
    public async Task<OrderDomain?> Execute(GetOrderByUsedComponentIdQuery orderByUsedComponentIdQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(orderByUsedComponentIdQuery, cancellationToken);

        var usedComponentExists = await getCustomerByIdStorage.GetUsedComponentById(orderByUsedComponentIdQuery.UsedComponentId, cancellationToken);
        
        if (usedComponentExists is null)
            throw new NotFoundByIdException(orderByUsedComponentIdQuery.UsedComponentId, "Используемый компонент");

        return await storage.GetOrderByUsedComponentId(orderByUsedComponentIdQuery.UsedComponentId, cancellationToken);
    }
}
