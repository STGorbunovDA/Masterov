namespace Masterov.Domain.Masterov.ProductType.UpdateProductType.Command;

public record UpdateProductTypeCommand(Guid ProductTypeId, string Name, string? Description);