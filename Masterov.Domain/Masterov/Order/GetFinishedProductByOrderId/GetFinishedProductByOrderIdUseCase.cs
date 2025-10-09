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
        var productionOrderExists = await getOrderByIdStorage.GetOrderById(getFinishedProductByOrderIdQuery.OrderId, cancellationToken);
        
        if (productionOrderExists is null)
            throw new NotFoundByIdException(getFinishedProductByOrderIdQuery.OrderId, "Ордер (заказ)");
        
        return await orderIdStorage.GetFinishedProductByOrderId(getFinishedProductByOrderIdQuery.OrderId, cancellationToken);
    }
}