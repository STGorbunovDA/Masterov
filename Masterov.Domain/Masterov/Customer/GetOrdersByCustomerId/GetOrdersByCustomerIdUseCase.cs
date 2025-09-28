using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Customer.GetCustomerById;
using Masterov.Domain.Masterov.Customer.GetOrdersByCustomerId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.GetOrdersByCustomerId;

public class GetOrdersByCustomerIdUseCase(IValidator<GetOrdersByCustomerIdQuery> validator,
    IGetOrdersByCustomerIdStorage storage, IGetCustomerByIdStorage getCustomerByIdStorage) 
    : IGetOrdersByCustomerIdUseCase
{
    public async Task<IEnumerable<ProductionOrderDomain>?> Execute(GetOrdersByCustomerIdQuery getOrdersByCustomerIdQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getOrdersByCustomerIdQuery, cancellationToken);

        var customerExists = await getCustomerByIdStorage.GetCustomerById(getOrdersByCustomerIdQuery.CustomerId, cancellationToken);
        
        if (customerExists is null)
            throw new NotFoundByIdException(getOrdersByCustomerIdQuery.CustomerId, "Заказчик");

        return await storage.GetOrdersByCustomerId(getOrdersByCustomerIdQuery.CustomerId, cancellationToken);
    }
}
