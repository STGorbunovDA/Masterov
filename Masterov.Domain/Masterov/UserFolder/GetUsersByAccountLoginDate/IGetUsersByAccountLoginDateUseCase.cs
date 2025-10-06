using Masterov.Domain.Masterov.UserFolder.GetUsersByAccountLoginDate.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.GetUsersByAccountLoginDate;

public interface IGetUsersByAccountLoginDateUseCase
{
    Task<IEnumerable<UserDomain>?> Execute(GetUsersByAccountLoginDateQuery accountLoginDateQuery, CancellationToken cancellationToken);
}