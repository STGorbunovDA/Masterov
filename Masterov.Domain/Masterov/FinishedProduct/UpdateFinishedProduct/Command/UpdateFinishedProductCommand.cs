namespace Masterov.Domain.Masterov.FinishedProduct.UpdateFinishedProduct.Command;

public record UpdateFinishedProductCommand(Guid FinishedProductId, string Name, string Type, decimal? Price, int? Width, int? Height, int? Depth, byte[]? Image, DateTime? CreatedAt, bool Elite, string Description);