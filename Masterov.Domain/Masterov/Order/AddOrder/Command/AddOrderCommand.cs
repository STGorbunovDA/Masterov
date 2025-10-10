namespace Masterov.Domain.Masterov.Order.AddOrder.Command;

public record AddOrderCommand(Guid FinishedProductId, string? Description, Guid CustomerId);