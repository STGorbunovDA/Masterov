namespace Masterov.Domain.Masterov.Supplier.AddSupplier.Command;

public record AddSupplierCommand(string Name, string Surname, string? Email, string? Phone, string? Address);