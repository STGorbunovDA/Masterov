using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.UpdateCustomer;

public interface IUpdateCustomerStorage
{
    Task<CustomerDomain> UpdateCustomer(Guid customerId, string name, string? email, string? phone, CancellationToken cancellationToken);
}