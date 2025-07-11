using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.ProductionOrder.GetFinishedProductAtOrder.Query;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrderById;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetFinishedProductAtOrder;

public class GetFinishedProductAtOrderUseCase(IValidator<GetFinishedProductAtOrderQuery> validator, IGetFinishedProductAtOrderStorage orderStorage, IGetProductionOrderByOrderIdStorage getProductionOrderByOrderIdStorage) : IGetFinishedProductAtOrderUseCase
{
    public async Task<FinishedProductDomain?> Execute(GetFinishedProductAtOrderQuery getFinishedProductAtOrderQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getFinishedProductAtOrderQuery, cancellationToken);
        var productionOrderExists = await getProductionOrderByOrderIdStorage.GetProductionOrderById(getFinishedProductAtOrderQuery.OrderId, cancellationToken);
        
        if (productionOrderExists is null)
            throw new NotFoundByIdException(getFinishedProductAtOrderQuery.OrderId, "Ордер (заказ)");
        
        return await orderStorage.GetFinishedProductAtOrder(getFinishedProductAtOrderQuery.OrderId, cancellationToken);
    }
}