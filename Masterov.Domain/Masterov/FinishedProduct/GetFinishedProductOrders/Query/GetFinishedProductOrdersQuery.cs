using Masterov.Domain.Extension;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductOrders.Query;

public record GetFinishedProductOrdersQuery(Guid FinishedProductId, DateTime? CreatedAt, DateTime? CompletedAt, ProductionOrderStatus Status, string? Description);