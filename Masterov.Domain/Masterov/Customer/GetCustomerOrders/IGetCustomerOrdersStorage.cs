using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.GetCustomerOrders;

public interface IGetCustomerOrdersStorage
{
    Task<IEnumerable<ProductionOrderDomain>?> GetCustomerOrders(Guid customerId, CancellationToken cancellationToken);
}