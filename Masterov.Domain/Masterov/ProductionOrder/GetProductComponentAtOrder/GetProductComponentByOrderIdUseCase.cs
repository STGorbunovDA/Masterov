using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.ProductionOrder.GetProductComponentAtOrder.Query;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrderById;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductComponentAtOrder;

public class GetProductComponentByOrderIdUseCase(IValidator<GetProductComponentByOrderIdQuery> validator, IGetProductComponentByOrderIdStorage orderIdStorage, IGetProductionOrderByOrderIdStorage getProductionOrderByOrderIdStorage) : IGetProductComponentByOrderIdUseCase
{
    public async Task<IEnumerable<ProductComponentDomain?>> Execute(GetProductComponentByOrderIdQuery getProductComponentByOrderIdQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getProductComponentByOrderIdQuery, cancellationToken);
        var productionOrderExists = await getProductionOrderByOrderIdStorage.GetProductionOrderById(getProductComponentByOrderIdQuery.OrderId, cancellationToken);
        
        if (productionOrderExists is null)
            throw new NotFoundByIdException(getProductComponentByOrderIdQuery.OrderId, "Ордер (заказ)");
        
        return await orderIdStorage.GetProductComponentAtOrder(getProductComponentByOrderIdQuery.OrderId, cancellationToken);
    }
}