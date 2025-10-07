using Masterov.Domain.Masterov.Order.GetCustomerByOrderId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetCustomerByOrderId;

public interface IGetCustomerByOrderIdUseCase
{
    Task<CustomerDomain?> Execute(GetCustomerByOrderIdQuery getCustomerByOrderIdQuery, CancellationToken cancellationToken);
}