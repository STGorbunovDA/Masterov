namespace Masterov.Domain.Masterov.Product.AddProduct.Command;

public record AddProductCommand(string Name, string Type, decimal? Price, int? Width, int? Height, int? Depth, byte[]? Content);