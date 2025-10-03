using FluentValidation;
using Masterov.Domain.Masterov.Customer.GetCustomersByUpdatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.GetCustomersByUpdatedAt;

public class GetCustomersByUpdatedAtUseCase(IValidator<GetCustomersByUpdatedAtQuery> validator,
    IGetCustomersByUpdatedAtStorage storage) : IGetCustomersByUpdatedAtUseCase
{
    public async Task<IEnumerable<CustomerDomain>?> Execute(GetCustomersByUpdatedAtQuery customersByUpdatedAtQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(customersByUpdatedAtQuery, cancellationToken);
        
        return await storage.GetCustomersByUpdatedAt(customersByUpdatedAtQuery.UpdatedAt, cancellationToken);
    }
}