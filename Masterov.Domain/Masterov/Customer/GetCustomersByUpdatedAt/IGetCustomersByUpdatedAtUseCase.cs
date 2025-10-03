using Masterov.Domain.Masterov.Customer.GetCustomersByUpdatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.GetCustomersByUpdatedAt;

public interface IGetCustomersByUpdatedAtUseCase
{
    Task<IEnumerable<CustomerDomain>?> Execute(GetCustomersByUpdatedAtQuery customersByUpdatedAtQuery, CancellationToken cancellationToken);
}