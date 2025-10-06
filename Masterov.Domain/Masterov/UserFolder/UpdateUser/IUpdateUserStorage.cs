using Masterov.Domain.Extension;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.UpdateUser;

public interface IUpdateUserStorage
{
    Task<UserDomain?> UpdateUser(Guid userId, string login, UserRole role, string? newPassword, DateTime? createdAt, Guid? customerId, CancellationToken cancellationToken);
}