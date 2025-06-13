using FluentValidation;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByCompletedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByCompletedAt;

public class GetProductionOrdersByCompletedAtUseCase(IValidator<GetProductionOrdersByCompletedAtQuery> validator,
    IGetProductionOrdersByCompletedAtStorage storage) 
    : IGetProductionOrdersByCompletedAtUseCase
{
    public async Task<IEnumerable<ProductionOrderDomain>?> Execute(GetProductionOrdersByCompletedAtQuery getProductionOrdersByCompletedAtQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getProductionOrdersByCompletedAtQuery, cancellationToken);
        
        return await storage.GetProductionOrdersByCompletedAt(getProductionOrdersByCompletedAtQuery.CompletedAt, cancellationToken);
    }
}