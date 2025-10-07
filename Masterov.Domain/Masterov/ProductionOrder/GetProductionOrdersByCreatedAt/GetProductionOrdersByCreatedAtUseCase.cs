using FluentValidation;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByCreatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByCreatedAt;

public class GetProductionOrdersByCreatedAtUseCase(IValidator<GetProductionOrdersByCreatedAtQuery> validator,
    IGetProductionOrdersByCreatedAtStorage storage) 
    : IGetProductionOrdersByCreatedAtUseCase
{
    public async Task<IEnumerable<OrderDomain>?> Execute(GetProductionOrdersByCreatedAtQuery productionOrdersByCreatedAtQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(productionOrdersByCreatedAtQuery, cancellationToken);
        
        return await storage.GetProductionOrdersByCreatedAt(productionOrdersByCreatedAtQuery.CreatedAt, cancellationToken);
    }
}