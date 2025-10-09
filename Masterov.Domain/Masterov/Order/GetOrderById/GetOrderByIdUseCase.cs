using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Order.GetOrderById.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetOrderById;

public class GetOrderByIdUseCase(IValidator<GetOrderByIdQuery> validator, IGetOrderByIdStorage storage) : IGetOrderByIdUseCase
{
    public async Task<OrderDomain?> Execute(GetOrderByIdQuery getOrderByIdQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getOrderByIdQuery, cancellationToken);
        var orderExists = await storage.GetOrderById(getOrderByIdQuery.OrderId, cancellationToken);
        
        if (orderExists is null)
            throw new NotFoundByIdException(getOrderByIdQuery.OrderId, "Заказ");
        
        return orderExists;
    }
}