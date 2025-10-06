using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.ChangeAccountLoginDateUserById;

public interface IChangeAccountLoginDateUserByIdStorage
{
    Task<UserDomain?> ChangeAccountLoginDateUserById(Guid userId, DateTime? accountLoginDate, CancellationToken cancellationToken);
}