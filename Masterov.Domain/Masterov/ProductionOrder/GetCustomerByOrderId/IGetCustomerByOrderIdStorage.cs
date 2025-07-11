using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetCustomerByOrderId;

public interface IGetCustomerByOrderIdStorage
{
    Task<CustomerDomain?> GetCustomerByOrderId(Guid orderId, CancellationToken cancellationToken);
}