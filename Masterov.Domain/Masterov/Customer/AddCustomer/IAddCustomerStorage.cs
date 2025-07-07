using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.AddCustomer;

public interface IAddCustomerStorage
{
    Task<CustomerDomain> AddCustomer(string name, string? email, string? phone, CancellationToken cancellationToken, Guid? orderId = null);
}