using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Order.GetCustomerByOrderId.Query;
using Masterov.Domain.Masterov.Order.GetOrderById;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetCustomerByOrderId;

public class GetCustomerByOrderIdUseCase(IValidator<GetCustomerByOrderIdQuery> validator, 
    IGetOrderByIdStorage getOrderByIdStorage,
    IGetCustomerByOrderIdStorage storage) : IGetCustomerByOrderIdUseCase
{
    public async Task<CustomerDomain?> Execute(GetCustomerByOrderIdQuery getCustomerByOrderIdQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getCustomerByOrderIdQuery, cancellationToken);
        var orderExists = await getOrderByIdStorage.GetOrderById(getCustomerByOrderIdQuery.OrderId, cancellationToken);
        
        if (orderExists is null)
            throw new NotFoundByIdException(getCustomerByOrderIdQuery.OrderId, "Ордер");
        
        
        
        return await storage.GetCustomerByOrderId(getCustomerByOrderIdQuery.OrderId, cancellationToken);
    }
}