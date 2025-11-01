using Masterov.Domain.Masterov.Customer.GetCustomersByName.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.GetCustomersByName;

public interface IGetCustomersByNameUseCase
{
    Task<IEnumerable<CustomerDomain?>> Execute(GetCustomersByNameQuery getCustomersByNameQuery, CancellationToken cancellationToken);
}