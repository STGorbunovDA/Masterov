using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetCustomerByOrderId;

public interface IGetCustomerByOrderIdStorage
{
    Task<CustomerDomain?> GetCustomerByOrderId(Guid orderId, CancellationToken cancellationToken);
}