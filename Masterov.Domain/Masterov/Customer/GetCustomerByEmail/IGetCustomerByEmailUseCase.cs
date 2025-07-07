using Masterov.Domain.Masterov.Customer.GetCustomerByEmail.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.GetCustomerByEmail;

public interface IGetCustomerByEmailUseCase
{
    Task<CustomerDomain?> Execute(GetCustomerByEmailQuery getCustomerByEmailQuery, CancellationToken cancellationToken);
}