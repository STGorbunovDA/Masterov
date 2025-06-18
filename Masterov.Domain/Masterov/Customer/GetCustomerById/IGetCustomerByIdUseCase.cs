using Masterov.Domain.Masterov.Customer.GetCustomerById.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.GetCustomerById;

public interface IGetCustomerByIdUseCase
{
    Task<CustomerDomain?> Execute(GetCustomerByIdQuery getCustomerByIdQuery, CancellationToken cancellationToken);
}