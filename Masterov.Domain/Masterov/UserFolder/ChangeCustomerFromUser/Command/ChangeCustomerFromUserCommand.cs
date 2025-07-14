namespace Masterov.Domain.Masterov.UserFolder.ChangeCustomerFromUser.Command;

public record ChangeCustomerFromUserCommand(Guid UserId, Guid CustomerId);