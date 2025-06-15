using Masterov.Domain.Masterov.UserFolder.DeleteUserById.Command;

namespace Masterov.Domain.Masterov.UserFolder.DeleteUserById;

public interface IDeleteUserByIdUseCase
{
    Task<bool> Execute(DeleteUserByIdCommand deleteUserByIdCommand, CancellationToken cancellationToken);
}