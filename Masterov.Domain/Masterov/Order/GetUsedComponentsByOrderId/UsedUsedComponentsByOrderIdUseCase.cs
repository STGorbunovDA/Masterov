using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Order.GetOrderById;
using Masterov.Domain.Masterov.Order.GetUsedComponentsByOrderId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetUsedComponentsByOrderId;

public class UsedUsedComponentsByOrderIdUseCase(IValidator<GetUsedComponentsByOrderIdQuery> validator, IGetUsedComponentsByOrderIdStorage orderIdStorage, IGetOrderByIdStorage getOrderByIdStorage) : IGetUsedComponentsByOrderIdUseCase
{
    public async Task<IEnumerable<UsedComponentDomain?>> Execute(GetUsedComponentsByOrderIdQuery getUsedComponentsByOrderIdQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getUsedComponentsByOrderIdQuery, cancellationToken);
        var orderExists = await getOrderByIdStorage.GetOrderById(getUsedComponentsByOrderIdQuery.OrderId, cancellationToken);
        
        if (orderExists is null)
            throw new NotFoundByIdException(getUsedComponentsByOrderIdQuery.OrderId, "Заказ)");
        
        return await orderIdStorage.GetUsedComponentsByOrderId(getUsedComponentsByOrderIdQuery.OrderId, cancellationToken);
    }
}