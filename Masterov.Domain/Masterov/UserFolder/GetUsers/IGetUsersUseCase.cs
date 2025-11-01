using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.GetUsers;

public interface IGetUsersUseCase
{
    Task<IEnumerable<UserDomain?>> Execute(CancellationToken cancellationToken);
}