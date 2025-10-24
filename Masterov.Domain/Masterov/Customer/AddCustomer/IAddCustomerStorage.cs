using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.AddCustomer;

public interface IAddCustomerStorage
{
    Task<CustomerDomain> AddCustomer(string name, string? phone, string? email, Guid? userId, CancellationToken cancellationToken);
}