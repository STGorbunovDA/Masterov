using Masterov.Domain.Masterov.UserFolder.GetUserById.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UserFolder.GetUserById;

public interface IGetUserByIdUseCase
{
    Task<UserDomain?> Execute(GetUserByIdQuery getUserByIdQuery, CancellationToken cancellationToken);
}