using Masterov.Domain.Masterov.Customer.GetCustomerOrders.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.GetCustomerOrders;

public interface IGetCustomerOrdersUseCase
{
    Task<IEnumerable<ProductionOrderDomain>?> Execute(GetCustomerOrdersQuery getCustomerOrdersQuery, CancellationToken cancellationToken);
}