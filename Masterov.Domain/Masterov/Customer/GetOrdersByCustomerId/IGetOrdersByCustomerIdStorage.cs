using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.GetOrdersByCustomerId;

public interface IGetOrdersByCustomerIdStorage
{
    Task<IEnumerable<OrderDomain>?> GetOrdersByCustomerId(Guid customerId, CancellationToken cancellationToken);
}