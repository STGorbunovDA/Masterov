using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.AddOrder;

public interface IAddOrderStorage
{
    Task<OrderDomain> AddOrder(Guid finishedProductId, Guid customerId, string? description, CancellationToken cancellationToken);
}