using FluentValidation;
using Masterov.Domain.Masterov.Customer.GetCustomersByCreatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.GetCustomersByCreatedAt;

public class GetCustomersByCreatedAtUseCase(IValidator<GetCustomersByCreatedAtQuery> validator,
    IGetCustomersByCreatedAtStorage storage) 
    : IGetCustomersByCreatedAtUseCase
{
    public async Task<IEnumerable<CustomerDomain>?> Execute(GetCustomersByCreatedAtQuery customersByCreatedAtQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(customersByCreatedAtQuery, cancellationToken);
        
        return await storage.GetCustomersByCreatedAt(customersByCreatedAtQuery.CreatedAt, cancellationToken);
    }
}