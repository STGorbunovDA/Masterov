using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Order.GetOrderById;
using Masterov.Domain.Masterov.Order.GetProductComponentByOrderId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetProductComponentByOrderId;

public class GetComponentsByOrderIdUseCase(IValidator<GetComponentsByOrderIdQuery> validator, IGetComponentsByOrderIdStorage orderIdStorage, IGetOrderByIdStorage getOrderByIdStorage) : IGetComponentsByOrderIdUseCase
{
    public async Task<IEnumerable<ComponentsDomain?>> Execute(GetComponentsByOrderIdQuery getComponentsByOrderIdQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getComponentsByOrderIdQuery, cancellationToken);
        var orderExists = await getOrderByIdStorage.GetOrderById(getComponentsByOrderIdQuery.OrderId, cancellationToken);
        
        if (orderExists is null)
            throw new NotFoundByIdException(getComponentsByOrderIdQuery.OrderId, "Заказ)");
        
        return await orderIdStorage.GetComponentsByOrderId(getComponentsByOrderIdQuery.OrderId, cancellationToken);
    }
}