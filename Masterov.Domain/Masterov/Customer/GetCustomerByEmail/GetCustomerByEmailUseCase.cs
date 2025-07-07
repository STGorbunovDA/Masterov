using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Customer.GetCustomerByEmail.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.GetCustomerByEmail;

public class GetCustomerByEmailUseCase(IValidator<GetCustomerByEmailQuery> validator, IGetCustomerByEmailStorage storage) : IGetCustomerByEmailUseCase
{
    public async Task<CustomerDomain?> Execute(GetCustomerByEmailQuery getCustomerByEmailQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getCustomerByEmailQuery, cancellationToken);
        var customerExists = await storage.GetCustomerByEmail(getCustomerByEmailQuery.Email, cancellationToken);
        
        if (customerExists is null)
            throw new NotFoundByNameException(getCustomerByEmailQuery.Email, "Заказчик");
        
        return customerExists;
    }
}