using Masterov.Domain.Masterov.UserFolder.LoginUser.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.LoginUser;

public interface ILoginUserUseCase
{
    Task<UserDomain> Execute(GetLoginUserQuery getLoginUserQuery, CancellationToken cancellationToken);
}