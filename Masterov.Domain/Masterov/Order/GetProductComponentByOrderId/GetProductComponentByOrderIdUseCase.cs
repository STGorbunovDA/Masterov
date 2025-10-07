using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Order.GetOrderById;
using Masterov.Domain.Masterov.Order.GetProductComponentByOrderId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetProductComponentByOrderId;

public class GetProductComponentByOrderIdUseCase(IValidator<GetProductComponentByOrderIdQuery> validator, IGetProductComponentByOrderIdStorage orderIdStorage, IGetOrderByOrderIdStorage getOrderByOrderIdStorage) : IGetProductComponentByOrderIdUseCase
{
    public async Task<IEnumerable<ProductComponentDomain?>> Execute(GetProductComponentByOrderIdQuery getProductComponentByOrderIdQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getProductComponentByOrderIdQuery, cancellationToken);
        var productionOrderExists = await getOrderByOrderIdStorage.GetOrderByOrderId(getProductComponentByOrderIdQuery.OrderId, cancellationToken);
        
        if (productionOrderExists is null)
            throw new NotFoundByIdException(getProductComponentByOrderIdQuery.OrderId, "Ордер (заказ)");
        
        return await orderIdStorage.GetProductComponentByOrderId(getProductComponentByOrderIdQuery.OrderId, cancellationToken);
    }
}