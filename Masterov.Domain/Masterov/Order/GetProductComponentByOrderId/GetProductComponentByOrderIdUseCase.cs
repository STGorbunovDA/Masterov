using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Order.GetOrderById;
using Masterov.Domain.Masterov.Order.GetProductComponentByOrderId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetProductComponentByOrderId;

public class GetProductComponentByOrderIdUseCase(IValidator<GetProductComponentByOrderIdQuery> validator, IGetProductComponentByOrderIdStorage orderIdStorage, IGetOrderByIdStorage getOrderByIdStorage) : IGetProductComponentByOrderIdUseCase
{
    public async Task<IEnumerable<ProductComponentDomain?>> Execute(GetProductComponentByOrderIdQuery getProductComponentByOrderIdQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getProductComponentByOrderIdQuery, cancellationToken);
        var orderExists = await getOrderByIdStorage.GetOrderById(getProductComponentByOrderIdQuery.OrderId, cancellationToken);
        
        if (orderExists is null)
            throw new NotFoundByIdException(getProductComponentByOrderIdQuery.OrderId, "Заказ)");
        
        return await orderIdStorage.GetProductComponentByOrderId(getProductComponentByOrderIdQuery.OrderId, cancellationToken);
    }
}