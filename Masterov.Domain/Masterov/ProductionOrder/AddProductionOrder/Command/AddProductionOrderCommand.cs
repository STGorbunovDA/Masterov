namespace Masterov.Domain.Masterov.ProductionOrder.AddProductionOrder.Command;

public record AddProductionOrderCommand(Guid FinishedProductId, string? Description, Guid? CustomerId, string? CustomerName, string? CustomerPhone, string? CustomerEmail);