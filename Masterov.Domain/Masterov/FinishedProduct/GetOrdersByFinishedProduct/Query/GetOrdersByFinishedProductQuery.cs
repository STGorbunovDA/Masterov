using Masterov.Domain.Extension;

namespace Masterov.Domain.Masterov.FinishedProduct.GetOrdersByFinishedProduct.Query;

public record GetOrdersByFinishedProductQuery(Guid FinishedProductId, DateTime? CreatedAt, DateTime? CompletedAt, ProductionOrderStatus Status, string? Description);