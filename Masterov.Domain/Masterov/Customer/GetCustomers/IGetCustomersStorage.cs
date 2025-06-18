using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.GetCustomers;

public interface IGetCustomersStorage
{
    Task<IEnumerable<CustomerDomain>> GetCustomers(CancellationToken cancellationToken);
}