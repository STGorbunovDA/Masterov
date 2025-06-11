namespace Masterov.Domain.Masterov.FinishedProduct.UpdateFinishedProduct.Command;

public record UpdateFinishedProductCommand(Guid FinishedProductId, string Name, decimal? Price, int? Width, int? Height, int? Depth, byte[]? Image);