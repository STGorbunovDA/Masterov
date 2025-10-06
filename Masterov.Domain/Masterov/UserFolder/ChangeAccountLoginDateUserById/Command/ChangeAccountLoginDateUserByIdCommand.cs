namespace Masterov.Domain.Masterov.UserFolder.ChangeUpdatedAtUserById.Command;

public record ChangeAccountLoginDateUserByIdCommand(Guid UserId, DateTime? AccountLoginDate);