using Masterov.Domain.Masterov.UserFolder.ChangeCustomerFromUser.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.ChangeCustomerFromUser;

public interface IChangeCustomerFromUserUseCase
{
    Task<UserDomain?> Execute(ChangeCustomerFromUserCommand customerFromUserCommand, CancellationToken cancellationToken);
}