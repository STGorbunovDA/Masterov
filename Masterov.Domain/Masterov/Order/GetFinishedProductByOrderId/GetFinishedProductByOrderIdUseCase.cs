using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Order.GetFinishedProductByOrderId.Query;
using Masterov.Domain.Masterov.Order.GetOrderById;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetFinishedProductByOrderId;

public class GetFinishedProductByOrderIdUseCase(IValidator<GetFinishedProductByOrderIdQuery> validator, IGetFinishedProductByOrderIdStorage orderIdStorage, IGetOrderByIdStorage getOrderByIdStorage) : IGetFinishedProductByOrderIdUseCase
{
    public async Task<FinishedProductDomain?> Execute(GetFinishedProductByOrderIdQuery getFinishedProductByOrderIdQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getFinishedProductByOrderIdQuery, cancellationToken);
        var orderExists = await getOrderByIdStorage.GetOrderById(getFinishedProductByOrderIdQuery.OrderId, cancellationToken);
        
        if (orderExists is null)
            throw new NotFoundByIdException(getFinishedProductByOrderIdQuery.OrderId, "Заказ)");
        
        return await orderIdStorage.GetFinishedProductByOrderId(getFinishedProductByOrderIdQuery.OrderId, cancellationToken);
    }
}