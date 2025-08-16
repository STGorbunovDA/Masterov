namespace Masterov.Domain.Masterov.Supplier.AddSupplier.Command;

public record AddSupplierCommand(string Name, string? Address, string? Phone);