using FluentValidation;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByStatus.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByStatus;

public class GetProductionOrdersByStatusUseCase(IValidator<GetProductionOrdersByStatusQuery> validator,
    IGetProductionOrdersByStatusStorage storage) 
    : IGetProductionOrdersByStatusUseCase
{
    public async Task<IEnumerable<ProductionOrderDomain>?> Execute(GetProductionOrdersByStatusQuery productionOrdersByStatusQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(productionOrdersByStatusQuery, cancellationToken);
        
        return await storage.GetProductionOrdersByStatus(productionOrdersByStatusQuery.Status, cancellationToken);
    }
}