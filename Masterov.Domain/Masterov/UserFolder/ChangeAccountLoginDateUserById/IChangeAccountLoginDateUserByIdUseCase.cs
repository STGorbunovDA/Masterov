using Masterov.Domain.Masterov.UserFolder.ChangeUpdatedAtUserById.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.ChangeAccountLoginDateUserById;

public interface IChangeAccountLoginDateUserByIdUseCase
{
    Task<UserDomain?> Execute(ChangeAccountLoginDateUserByIdCommand changeAccountLoginDateUserByIdCommand, CancellationToken cancellationToken);
}