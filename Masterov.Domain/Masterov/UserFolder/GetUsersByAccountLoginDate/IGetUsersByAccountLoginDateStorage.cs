using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.GetUsersByAccountLoginDate;

public interface IGetUsersByAccountLoginDateStorage
{
    Task<IEnumerable<UserDomain>?> GetUsersByAccountLoginDate(DateTime? accountLoginDate, CancellationToken cancellationToken);
}