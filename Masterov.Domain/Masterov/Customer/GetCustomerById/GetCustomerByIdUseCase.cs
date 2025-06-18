using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Customer.GetCustomerById.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.GetCustomerById;

public class GetCustomerByIdUseCase(IValidator<GetCustomerByIdQuery> validator, IGetCustomerByIdStorage storage) : IGetCustomerByIdUseCase
{
    public async Task<CustomerDomain?> Execute(GetCustomerByIdQuery getCustomerByIdQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getCustomerByIdQuery, cancellationToken);
        var customerExists = await storage.GetCustomerById(getCustomerByIdQuery.CustomerId, cancellationToken);
        
        if (customerExists is null)
            throw new NotFoundByIdException(getCustomerByIdQuery.CustomerId, "Заказчик");
        
        return customerExists;
    }
}