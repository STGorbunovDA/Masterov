namespace Masterov.Domain.Masterov.Customer.UpdateCustomer.Command;

public record UpdateCustomerCommand(Guid CustomerId, string Name, string? Email, string? Phone, DateTime? CreatedAt);