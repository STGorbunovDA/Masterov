using Masterov.Domain.Masterov.Customer.GetCustomersByCreatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.GetCustomersByCreatedAt;

public interface IGetCustomersByCreatedAtUseCase
{
    Task<IEnumerable<CustomerDomain>?> Execute(GetCustomersByCreatedAtQuery customersByCreatedAtQuery, CancellationToken cancellationToken);
}