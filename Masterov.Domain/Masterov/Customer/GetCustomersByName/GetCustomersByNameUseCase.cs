using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Customer.GetCustomersByName.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.GetCustomersByName;

public class GetCustomersByNameUseCase(IValidator<GetCustomersByNameQuery> validator, IGetCustomerByNameStorage storage) : IGetCustomersByNameUseCase
{
    public async Task<IEnumerable<CustomerDomain?>> Execute(GetCustomersByNameQuery getCustomersByNameQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getCustomersByNameQuery, cancellationToken);
        var customersExists = await storage.GetCustomersByName(getCustomersByNameQuery.CustomerName, cancellationToken);
        
        if (customersExists is null)
            throw new NotFoundByNameException(getCustomersByNameQuery.CustomerName, "Заказчик");
        
        return customersExists;
    }
}