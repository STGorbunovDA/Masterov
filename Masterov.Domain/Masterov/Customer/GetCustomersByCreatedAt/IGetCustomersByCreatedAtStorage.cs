using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.GetCustomersByCreatedAt;

public interface IGetCustomersByCreatedAtStorage
{
    Task<IEnumerable<CustomerDomain>?> GetCustomersByCreatedAt(DateTime createdAt, CancellationToken cancellationToken);
}