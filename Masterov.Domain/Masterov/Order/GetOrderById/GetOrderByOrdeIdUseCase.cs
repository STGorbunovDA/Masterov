using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Order.GetOrderById.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetOrderById;

public class GetOrderByOrdeIdUseCase(IValidator<GetOrderByOrderIdQuery> validator, IGetOrderByOrderIdStorage storage) : IGetOrderByOrdeIdUseCase
{
    public async Task<OrderDomain?> Execute(GetOrderByOrderIdQuery getOrderByOrderIdQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getOrderByOrderIdQuery, cancellationToken);
        var productionOrderExists = await storage.GetOrderByOrderId(getOrderByOrderIdQuery.OrderId, cancellationToken);
        
        if (productionOrderExists is null)
            throw new NotFoundByIdException(getOrderByOrderIdQuery.OrderId, "Ордер (заказ)");
        
        return productionOrderExists;
    }
}