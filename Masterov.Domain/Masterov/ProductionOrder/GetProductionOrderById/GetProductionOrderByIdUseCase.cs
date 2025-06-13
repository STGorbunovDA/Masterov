using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrderById.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductionOrderById;

public class GetProductionOrderByIdUseCase(IValidator<GetProductionOrderByIdQuery> validator, IGetProductionOrderByIdStorage storage) : IGetProductionOrderByIdUseCase
{
    public async Task<ProductionOrderDomain?> Execute(GetProductionOrderByIdQuery getProductionOrderByIdQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getProductionOrderByIdQuery, cancellationToken);
        var productionOrderExists = await storage.GetProductionOrderById(getProductionOrderByIdQuery.OrderId, cancellationToken);
        
        if (productionOrderExists is null)
            throw new NotFoundByIdException(getProductionOrderByIdQuery.OrderId, "Ордер (заказ)");
        
        return productionOrderExists;
    }
}