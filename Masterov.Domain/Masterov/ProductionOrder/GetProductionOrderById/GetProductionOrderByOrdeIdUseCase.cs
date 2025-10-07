using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrderById.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductionOrderById;

public class GetProductionOrderByOrdeIdUseCase(IValidator<GetProductionOrderByOrderIdQuery> validator, IGetProductionOrderByOrderIdStorage storage) : IGetProductionOrderByOrdeIdUseCase
{
    public async Task<OrderDomain?> Execute(GetProductionOrderByOrderIdQuery getProductionOrderByOrderIdQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getProductionOrderByOrderIdQuery, cancellationToken);
        var productionOrderExists = await storage.GetProductionOrderById(getProductionOrderByOrderIdQuery.OrderId, cancellationToken);
        
        if (productionOrderExists is null)
            throw new NotFoundByIdException(getProductionOrderByOrderIdQuery.OrderId, "Ордер (заказ)");
        
        return productionOrderExists;
    }
}