using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.ProductionOrder.GetFinishedProductByOrderId.Query;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrderById;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetFinishedProductByOrderId;

public class GetFinishedProductByOrderIdUseCase(IValidator<GetFinishedProductByOrderIdQuery> validator, IGetFinishedProductByOrderIdStorage orderIdStorage, IGetProductionOrderByOrderIdStorage getProductionOrderByOrderIdStorage) : IGetFinishedProductByOrderIdUseCase
{
    public async Task<FinishedProductDomain?> Execute(GetFinishedProductByOrderIdQuery getFinishedProductByOrderIdQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getFinishedProductByOrderIdQuery, cancellationToken);
        var productionOrderExists = await getProductionOrderByOrderIdStorage.GetProductionOrderById(getFinishedProductByOrderIdQuery.OrderId, cancellationToken);
        
        if (productionOrderExists is null)
            throw new NotFoundByIdException(getFinishedProductByOrderIdQuery.OrderId, "Ордер (заказ)");
        
        return await orderIdStorage.GetFinishedProductByOrderId(getFinishedProductByOrderIdQuery.OrderId, cancellationToken);
    }
}