using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.GetCustomers;

public interface IGetCustomersUseCase
{
    Task<IEnumerable<CustomerDomain?>> Execute(CancellationToken cancellationToken);
}