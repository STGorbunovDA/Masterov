using Masterov.Domain.Masterov.ProductionOrder.GetCustomerByOrderId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetCustomerByOrderId;

public interface IGetCustomerByOrderIdUseCase
{
    Task<CustomerDomain?> Execute(GetCustomerByOrderIdQuery getCustomerByOrderIdQuery, CancellationToken cancellationToken);
}