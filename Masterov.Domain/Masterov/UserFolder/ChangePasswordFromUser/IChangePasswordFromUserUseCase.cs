using Masterov.Domain.Masterov.UserFolder.ChangePasswordFromUser.Command;

namespace Masterov.Domain.Masterov.UserFolder.ChangePasswordFromUser;

public interface IChangePasswordFromUserUseCase
{
    Task<bool> Execute(ChangePasswordFromUserCommand changePasswordFromUserCommand, CancellationToken cancellationToken);
}