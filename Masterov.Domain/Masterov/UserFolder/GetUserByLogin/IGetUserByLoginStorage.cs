using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.GetUserByLogin;

public interface IGetUserByLoginStorage
{
    Task<UserDomain?> GetUserByLogin(string login, CancellationToken cancellationToken);
}