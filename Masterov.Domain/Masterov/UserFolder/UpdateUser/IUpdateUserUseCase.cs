using Masterov.Domain.Masterov.UserFolder.UpdateUser.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.UpdateUser;

public interface IUpdateUserUseCase
{
    Task<UserDomain?> Execute(UpdateUserCommand command, CancellationToken cancellationToken);
}