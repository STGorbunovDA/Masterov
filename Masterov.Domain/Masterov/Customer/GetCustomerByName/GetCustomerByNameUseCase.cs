using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Customer.GetCustomerByName.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.GetCustomerByName;

public class GetCustomerByNameUseCase(IValidator<GetCustomerByNameQuery> validator, IGetCustomerByNameStorage storage) : IGetCustomerByNameUseCase
{
    public async Task<CustomerDomain?> Execute(GetCustomerByNameQuery getCustomerByNameQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getCustomerByNameQuery, cancellationToken);
        var customerExists = await storage.GetCustomerByName(getCustomerByNameQuery.CustomerName, cancellationToken);
        
        if (customerExists is null)
            throw new NotFoundByNameException(getCustomerByNameQuery.CustomerName, "Заказчик");
        
        return customerExists;
    }
}