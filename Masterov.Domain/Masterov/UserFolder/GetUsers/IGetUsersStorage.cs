using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.GetUsers;

public interface IGetUsersStorage
{
    Task<IEnumerable<UserDomain?>> GetUsers(CancellationToken cancellationToken);
}