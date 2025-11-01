using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.GetCustomersByName;

public interface IGetCustomerByNameStorage
{
    Task<IEnumerable<CustomerDomain?>> GetCustomersByName(string customerName, CancellationToken cancellationToken);
}