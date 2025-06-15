using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.GetUserById;

public interface IGetUserByIdStorage
{
    Task<UserDomain?> GetUserById(Guid userId, CancellationToken cancellationToken);
}