namespace Masterov.Domain.Masterov.Supplier.UpdateSupplier.Command;

public record UpdateSupplierCommand(Guid SupplierId, string Name, string? Address, string? Phone);