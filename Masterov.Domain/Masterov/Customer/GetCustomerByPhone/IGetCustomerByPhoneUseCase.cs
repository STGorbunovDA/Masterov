using Masterov.Domain.Masterov.Customer.GetCustomerByPhone.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.GetCustomerByPhone;

public interface IGetCustomerByPhoneUseCase
{
    Task<CustomerDomain?> Execute(GetCustomerByPhoneQuery getCustomerByPhoneQuery, CancellationToken cancellationToken);
}