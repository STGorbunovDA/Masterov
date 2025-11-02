namespace Masterov.Domain.Masterov.Supplier.UpdateSupplier.Command;

public record UpdateSupplierCommand(Guid SupplierId, string Name, string Surname, string? Email, string? Phone, string? Address, DateTime? CreatedAt);