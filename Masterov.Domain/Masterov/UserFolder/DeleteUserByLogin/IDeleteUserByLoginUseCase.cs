using Masterov.Domain.Masterov.UserFolder.DeleteUserByLogin.Command;

namespace Masterov.Domain.Masterov.UserFolder.DeleteUserByLogin;

public interface IDeleteUserByLoginUseCase
{
    Task<bool> Execute(DeleteUserByLoginCommand deleteUserByLoginCommand, CancellationToken cancellationToken);
}