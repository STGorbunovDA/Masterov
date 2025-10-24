namespace Masterov.Domain.Masterov.UserFolder.RegisterUser.Command;

public record RegisterUserCommand(string Email, string Password, string Phone, Guid? CustomerId);