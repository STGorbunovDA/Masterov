namespace Masterov.Domain.Masterov.FinishedProduct.AddFinishedProduct.Command;

public record AddFinishedProductCommand(string Name, decimal? Price, int? Width, int? Height, int? Depth, byte[]? Image);