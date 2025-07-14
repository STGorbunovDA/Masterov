using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.ChangeCustomerFromUser;

public interface IChangeCustomerFromUserStorage
{
    Task<UserDomain?> ChangeCustomerFromUser(Guid userId, Guid customerId, CancellationToken cancellationToken);
}