using Masterov.Domain.Extension;

namespace Masterov.Domain.Masterov.Order.GetOrdersByStatus.Query;

public record GetOrdersByStatusQuery(OrderStatus Status);