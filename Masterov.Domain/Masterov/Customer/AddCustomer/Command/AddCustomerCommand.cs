namespace Masterov.Domain.Masterov.Customer.AddCustomer.Command;

public record AddCustomerCommand(string Name, string? Email, string? Phone);