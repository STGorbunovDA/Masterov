using Masterov.Domain.Masterov.Customer.GetCustomerByName.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.GetCustomerByName;

public interface IGetCustomerByNameUseCase
{
    Task<CustomerDomain?> Execute(GetCustomerByNameQuery getCustomerByNameQuery, CancellationToken cancellationToken);
}