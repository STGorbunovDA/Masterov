using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Customer.GetCustomerByPhone.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.GetCustomerByPhone;

public class GetCustomerByPhoneUseCase(IValidator<GetCustomerByPhoneQuery> validator, IGetCustomerByPhoneStorage storage) : IGetCustomerByPhoneUseCase
{
    public async Task<CustomerDomain?> Execute(GetCustomerByPhoneQuery getCustomerByPhoneQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getCustomerByPhoneQuery, cancellationToken);
        var customerExists = await storage.GetCustomerByPhone(getCustomerByPhoneQuery.Phone, cancellationToken);
        
        if (customerExists is null)
            throw new NotFoundByPhoneException(getCustomerByPhoneQuery.Phone);
        
        return customerExists;
    }
}