using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.ChangePasswordFromUser;

public interface IChangePasswordFromUserStorage
{
    Task<bool> ChangePasswordFromUser(Guid userId, string newPassword, CancellationToken cancellationToken);
}