using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.GetCustomersByUpdatedAt;

public interface IGetCustomersByUpdatedAtStorage
{
    Task<IEnumerable<CustomerDomain>?> GetCustomersByUpdatedAt(DateTime? updatedAt, CancellationToken cancellationToken);
}