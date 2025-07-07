using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.GetCustomerByPhone;

public interface IGetCustomerByPhoneStorage
{
    Task<CustomerDomain?> GetCustomerByPhone(string customerPhone, CancellationToken cancellationToken);
}