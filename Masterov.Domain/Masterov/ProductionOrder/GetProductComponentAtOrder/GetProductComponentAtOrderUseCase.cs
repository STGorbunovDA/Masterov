using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.ProductionOrder.GetProductComponentAtOrder.Query;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrderById;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductComponentAtOrder;

public class GetProductComponentAtOrderUseCase(IValidator<GetProductComponentAtOrderQuery> validator, IGetProductComponentAtOrderStorage orderStorage, IGetProductionOrderByIdStorage getProductionOrderByIdStorage) : IGetProductComponentAtOrderUseCase
{
    public async Task<IEnumerable<ProductComponentDomain?>> Execute(GetProductComponentAtOrderQuery getProductComponentAtOrderQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getProductComponentAtOrderQuery, cancellationToken);
        var productionOrderExists = await getProductionOrderByIdStorage.GetProductionOrderById(getProductComponentAtOrderQuery.OrderId, cancellationToken);
        
        if (productionOrderExists is null)
            throw new NotFoundByIdException(getProductComponentAtOrderQuery.OrderId, "Ордер (заказ)");
        
        return await orderStorage.GetProductComponentAtOrder(getProductComponentAtOrderQuery.OrderId, cancellationToken);
    }
}