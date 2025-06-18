using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.GetCustomerByName;

public interface IGetCustomerByNameStorage
{
    Task<CustomerDomain?> GetCustomerByName(string customerName, CancellationToken cancellationToken);
}