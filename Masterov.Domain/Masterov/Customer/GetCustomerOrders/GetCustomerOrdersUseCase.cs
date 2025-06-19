using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Customer.GetCustomerById;
using Masterov.Domain.Masterov.Customer.GetCustomerOrders.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.GetCustomerOrders;

public class GetCustomerOrdersUseCase(IValidator<GetCustomerOrdersQuery> validator,
    IGetCustomerOrdersStorage storage, IGetCustomerByIdStorage getCustomerByIdStorage) 
    : IGetCustomerOrdersUseCase
{
    public async Task<IEnumerable<ProductionOrderDomain>?> Execute(GetCustomerOrdersQuery getCustomerOrdersQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getCustomerOrdersQuery, cancellationToken);

        var customerExists = await getCustomerByIdStorage.GetCustomerById(getCustomerOrdersQuery.CustomerId, cancellationToken);
        
        if (customerExists is null)
            throw new NotFoundByIdException(getCustomerOrdersQuery.CustomerId, "Заказчик");

        return await storage.GetCustomerOrders(getCustomerOrdersQuery.CustomerId, cancellationToken);
    }
}
