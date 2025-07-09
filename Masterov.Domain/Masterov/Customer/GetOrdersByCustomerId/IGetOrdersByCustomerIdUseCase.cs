using Masterov.Domain.Masterov.Customer.GetOrdersByCustomerId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.GetOrdersByCustomerId;

public interface IGetOrdersByCustomerIdUseCase
{
    Task<IEnumerable<ProductionOrderDomain>?> Execute(GetOrdersByCustomerIdQuery getOrdersByCustomerIdQuery, CancellationToken cancellationToken);
}