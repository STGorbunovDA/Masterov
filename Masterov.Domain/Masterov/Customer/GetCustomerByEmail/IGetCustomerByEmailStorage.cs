using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.GetCustomerByEmail;

public interface IGetCustomerByEmailStorage
{
    Task<CustomerDomain?> GetCustomerByEmail(string customerEmail, CancellationToken cancellationToken);
}