using Masterov.Domain.Extension;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetOrdersByFinishedProduct;

public interface IGetOrdersByFinishedProductStorage
{
    Task<IEnumerable<OrderDomain>?> GetFinishedProductOrders(Guid finishedProductId, DateTime? createdAt, DateTime? completedAt, ProductionOrderStatus status, string? description, CancellationToken cancellationToken);
}