using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.ProductionOrder.GetCustomerByOrderId.Query;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrderById;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetCustomerByOrderId;

public class GetCustomerByOrderIdUseCase(IValidator<GetCustomerByOrderIdQuery> validator, 
    IGetProductionOrderByOrderIdStorage getProductionOrderByOrderIdStorage,
    IGetCustomerByOrderIdStorage storage) : IGetCustomerByOrderIdUseCase
{
    public async Task<CustomerDomain?> Execute(GetCustomerByOrderIdQuery getCustomerByOrderIdQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getCustomerByOrderIdQuery, cancellationToken);
        var orderExists = await getProductionOrderByOrderIdStorage.GetProductionOrderById(getCustomerByOrderIdQuery.OrderId, cancellationToken);
        
        if (orderExists is null)
            throw new NotFoundByIdException(getCustomerByOrderIdQuery.OrderId, "Ордер");
        
        
        
        return await storage.GetCustomerByOrderId(getCustomerByOrderIdQuery.OrderId, cancellationToken);
    }
}