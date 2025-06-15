using Masterov.Domain.Masterov.UserFolder.GetUserByLogin.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.GetUserByLogin;

public interface IGetUserByLoginUseCase
{
    Task<UserDomain?> Execute(GetUserByLoginQuery getUserByLoginQuery, CancellationToken cancellationToken);
}