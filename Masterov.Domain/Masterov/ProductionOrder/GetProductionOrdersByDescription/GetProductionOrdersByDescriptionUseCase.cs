using FluentValidation;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByDescription.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByDescription;

public class GetProductionOrdersByDescriptionUseCase(IValidator<GetProductionOrdersByDescriptionQuery> validator,
    IGetProductionOrdersByDescriptionStorage storage) 
    : IGetProductionOrdersByDescriptionUseCase
{
    public async Task<IEnumerable<OrderDomain>?> Execute(GetProductionOrdersByDescriptionQuery getProductionOrdersByDescriptionQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getProductionOrdersByDescriptionQuery, cancellationToken);
        
        return await storage.GetProductionOrdersByDescription(getProductionOrdersByDescriptionQuery.Description, cancellationToken);
    }
}