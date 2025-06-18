using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.GetCustomerById;

public interface IGetCustomerByIdStorage
{
    Task<CustomerDomain?> GetCustomerById(Guid сustomerId, CancellationToken cancellationToken);
}