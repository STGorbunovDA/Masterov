using Masterov.Domain.Masterov.Customer.GetOrdersByCustomerId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.GetOrdersByCustomerId;

public interface IGetOrdersByCustomerIdUseCase
{
    Task<IEnumerable<OrderDomain>?> Execute(GetOrdersByCustomerIdQuery getOrdersByCustomerIdQuery, CancellationToken cancellationToken);
}